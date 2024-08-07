using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Sound/Pipki")]
public class PipkiPopkiStorage : ScriptableObject
{
    [SerializeField] private AudioClip[] _clips;
    [SerializeField] private AudioClip _spawn;
    [SerializeField] private AudioClip _despawn;
    public AudioClip GetRandom() => _clips[Random.Range(0, _clips.Length)];

    public AudioClip Spawn => _spawn;
    public AudioClip Despawn => _despawn;

}
