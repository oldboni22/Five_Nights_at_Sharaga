using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class SurrondAudioPlayer : MonoBehaviour
{
    private string _soundId;
    public string SoundId => _soundId;
    private Pool _pool;
    [SerializeField] private AudioSource _source;

    public void SetPool(Pool pool) => _pool = pool;

    public void PlayAudio(AudioClip clip, Vector2 position)
    {
        transform.position = position;
        float duration = clip.length;
        StartCoroutine(StartPlaying(duration, clip));
    }
    public void PlayAudio(float duration ,AudioClip clip, Vector2 position)
    {
        transform.position = position;
        StartCoroutine(StartPlaying(duration, clip));
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
        _soundId = null;
        _source.Stop();
        _source.clip = null;
        transform.position = new Vector2(-1000,-1000);
        _pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<SurrondAudioPlayer>
    {
        readonly List<SurrondAudioPlayer> _players = new List<SurrondAudioPlayer>();

        protected override void OnCreated(SurrondAudioPlayer item)
        { 
            base.OnCreated(item);
            _players.Add(item);
            item.SetPool(this);
        }


        public void PlayAudio(AudioClip clip, Vector2 position) 
        {
            var player = this.Spawn();
            player.PlayAudio(clip, position);
        }
        public void PlayAudio(float duration,AudioClip clip, Vector2 position) 
        {
            var player = this.Spawn();
            player.PlayAudio(duration,clip, position);
        }
        public void StopAudio(AudioClip clip)
        {
            var soundId = clip.name;
           
            foreach(var player in _players.Where(x => x.SoundId == soundId))
            {
                player.StopPlaying();
            }
        }

    }

}

