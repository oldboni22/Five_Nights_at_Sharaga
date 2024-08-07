using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Random")]
public class RandomSoundStorage : ScriptableObject
{
    [SerializeField] private AudioClip[] _sounds;
    public AudioClip GetRandom() => _sounds[Random.Range(0, _sounds.Length)]; 
}
