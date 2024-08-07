using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RandomSounds : MonoBehaviour, IClockable
{
    [Inject] private RandomSoundStorage _sounds;
    [Inject] private AudioPlayer.Pool _audioPool;
    [SerializeField] private ushort _limit;
    [SerializeField] private ushort _count;

    public void Tick()
    {
        _count += Convert.ToUInt16(UnityEngine.Random.Range(1, 110));
        if(_count >= _limit)
        {
            _count = 0;
            PlayRandom();
        }
    }

    void PlayRandom()
    {
        _audioPool.PlayAudio(_sounds.GetRandom(), 1, 145);
    }
}
