using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Clip")]
public class SoundClip : ScriptableObject, IStoreable
{
    [SerializeField] private string _id;
    [SerializeField] private AudioClip _clip;

    public AudioClip Clip => _clip;
    public string Id => _id;
}