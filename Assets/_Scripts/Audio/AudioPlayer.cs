using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class AudioPlayer : MonoBehaviour
{
    private string _soundId;
    public string SoundId => _soundId;
    private Pool _pool;
    [SerializeField] private AudioSource _source;

    public void SetPool(Pool pool) => _pool = pool;

    public void PlayAudio(AudioClip clip)
    {
        float duration = clip.length;
        StartCoroutine(StartPlaying(duration, clip));
    }
    public void PlayAudio(float duration ,AudioClip clip)
    {
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
        _pool.Despawn(this);
    }

    public class Pool : MonoMemoryPool<AudioPlayer>
    {
        readonly List<AudioPlayer> _players = new List<AudioPlayer>();

        protected override void OnCreated(AudioPlayer item)
        { 
            base.OnCreated(item);
            _players.Add(item);
            item.SetPool(this);
        }


        public void PlayAudio(AudioClip clip) 
        {
            var player = this.Spawn();
            player.PlayAudio(clip);
        }
        public void PlayAudio(float duration,AudioClip clip) 
        {
            var player = this.Spawn();
            player.PlayAudio(duration,clip);
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

