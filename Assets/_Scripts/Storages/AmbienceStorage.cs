using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Ambience")]
public class AmbienceStorage : ScriptableObject
{
    [SerializeField] private AudioClip[] _saneClips;
    [SerializeField] private AudioClip[] _insaneClips;

    public AudioClip GetRandom(float sanity)
    {
        float insaneChance = 1 - sanity / 100;
        float rand = Random.Range(0f, 1f);

        if(rand > insaneChance)
            return _saneClips[Random.Range(0, _saneClips.Length)];
        else 
            return _insaneClips[Random.Range(0, _insaneClips.Length)];
    }
}
