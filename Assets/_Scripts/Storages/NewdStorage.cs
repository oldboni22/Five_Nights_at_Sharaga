using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="Sound/NewD")]
public class NewdStorage : ScriptableObject
{
    [SerializeField] private AudioClip[] _clips;
    public AudioClip GetRandom() => _clips[Random.Range(0, _clips.Length)];

}
