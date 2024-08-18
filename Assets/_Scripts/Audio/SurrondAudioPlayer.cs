using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

public class SurrondAudioPlayer : MonoBehaviour
{
    private string _soundId;
    public string SoundId => _soundId;
    private Pool _pool;
    [SerializeField] internal AudioSource _source;

    public void SetPool(Pool pool) => _pool = pool;

    public void PlayAudio(AudioClip clip, Vector2 position)
    {
        transform.position = position;
        float duration = clip.length;
        StartCoroutine(StartPlaying(duration, clip));
    }
    internal void OnCancelCall(string id)
    {
        if (gameObject.activeInHierarchy is false)
            return;
        if (id == _soundId ^ id == null)
        {
            StopPlaying();
        }

    }
    public void LoopAudio(AudioClip clip, Vector2 position)
    {
        transform.position = position;
        _source.clip = clip;
        _source.loop = true;
        _soundId = clip.name;
        _source.Play();
    }
    public void StopPlaying()
    {
        StopAllCoroutines();
        Reset();
    }

    IEnumerator StartPlaying(float duration, AudioClip clip)
    {
        _soundId = clip.name;

        _source.clip = clip;
        _source.Play();

        yield return new WaitForSeconds(duration);
        Reset();
    }

    private void Reset()
    {
        _source.volume = 1.0f;
        _source.loop = false;
        _soundId = null;
        _source.Stop();
        _source.clip = null;
        transform.position = new Vector2(-1000,-1000);
        _pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<SurrondAudioPlayer>
    {
        private event Action<string> _onCancellCall;
        readonly List<SurrondAudioPlayer> _players = new List<SurrondAudioPlayer>();

        protected override void OnCreated(SurrondAudioPlayer item)
        { 
            base.OnCreated(item);
            _players.Add(item);
            _onCancellCall += item.OnCancelCall;
            item.SetPool(this);
        }

        public void PlayAudio(AudioClip clip, Vector2 position,float volume,int prio) 
        {
            var player = this.Spawn();
            player._source.priority = prio;
            player._source.volume = volume;
            player.PlayAudio(clip, position);
        }
        public void StopAudio(string? clipName)
        {
            _onCancellCall?.Invoke(clipName);
        }
        public void LoopAudio(AudioClip clip, Vector2 position, float volume, int prio)
        {
            var player = Spawn();
            player._source.priority = prio;
            player._source.volume = volume;
            player.LoopAudio(clip, position);
        }

    }

}

