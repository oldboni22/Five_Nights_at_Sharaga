using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Ambience : MonoBehaviour
{
    [Inject] IPlayer _player;
    [Inject] AmbienceStorage _storage;
    private AudioSource _audioSource;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        StartCoroutine(PlayAmbience());
    }
    IEnumerator PlayAmbience()
    {
        var clip = _storage.GetRandom(_player.Sanity.CurSanity);
        _audioSource.clip = clip;

        _audioSource.Play();

        yield return new WaitForSeconds(clip.length);

        StartCoroutine(PlayAmbience());
    }
}


