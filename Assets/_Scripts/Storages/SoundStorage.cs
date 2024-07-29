using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Sound/Clips storage", fileName = "SoundClips")]
public class SoundStorage : Storage<SoundClip>
{
    [SerializeField] private SoundClip[] _clips;
    protected override SoundClip[] Members => _clips;
}
