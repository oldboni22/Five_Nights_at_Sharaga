using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;
using System;

public class AudioPlayer : MonoBehaviour
{
    [SerializeField] internal string _clipID;
    private Pool _pool;
    [SerializeField] internal AudioSource _source;

    public void SetPool(Pool pool) => _pool = pool;

    public void PlayAudio(AudioClip clip)
    {
        float duration = clip.length;
        StartCoroutine(StartPlaying(duration, clip));
    }   

    internal void OnCancelCall(string id)
    {
        if (gameObject.activeInHierarchy is false)
            return;
        if (id == _clipID ^ id == null)
        {
            StopPlaying();
        }
            
    }
    public void StopPlaying()
    {
        StopAllCoroutines();
        Reset();
    }

    IEnumerator StartPlaying(float duration, AudioClip clip)
    {
        _clipID = clip.name;

        _source.clip = clip;
        _source.Play();

        yield return new WaitForSeconds(duration);
        Reset();
    }

    private void Reset()
    {
        _source.loop = false;
        _clipID = null;
        _source.Stop();
        _source.clip = null;
        _pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<AudioPlayer>
    {

        private event Action<string> _onCancellCall;
        readonly List<AudioPlayer> _players = new List<AudioPlayer>();
        [Inject] private SoundStorage _soundStorage;

        protected override void OnCreated(AudioPlayer item)
        { 
            base.OnCreated(item);
            _onCancellCall += item.OnCancelCall;
            _players.Add(item);
            item.SetPool(this);
        }

        public void PlayAudio(AudioClip clip,float volume, int prio) 
        {
            var player = this.Spawn();
            player._source.volume = volume;
            player._source.priority = prio;
            player.PlayAudio(clip);
        }
        public void PlayAudio(string clip,float volume, int prio) 
        {
            var player = this.Spawn();
            player._source.volume = volume;
            player._source.priority = prio;
            player.PlayAudio(_soundStorage.GetMemberById(clip).Clip);
        }

        public void LoopAudio(AudioClip clip,float volume, int prio)
        {
            var player = this.Spawn();
            player._clipID = clip.name;
            player._source.clip = clip;
            player._source.loop = true;
            player._source.volume = volume;
            player._source.priority = prio;
            player._source.Play();
        }

#nullable enable
        public void StopAudioByClipId(string id)
        {
            string name = _soundStorage.GetMemberById(id).Clip.name;
            _onCancellCall.Invoke(name);
        }
        public void StopAudio(string? id)
        {
            _onCancellCall.Invoke(id);
        }
#nullable disable

    }

}

