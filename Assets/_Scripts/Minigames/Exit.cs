using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace minigames.jumper
{

    public class Exit : MonoBehaviour
    {
        [SerializeField] private GameObject _canvas;
        [Inject] private AudioPlayer.Pool _audio;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            _audio.StopAudio(null);
            Destroy(collision.gameObject);
            
            _canvas.SetActive(true);
            _audio.PlayAudio("mystery",1,255);
            StartCoroutine(Delay());
        }

        IEnumerator Delay()
        {
            yield return new WaitForSeconds(3.5f);
            _audio.StopAudioByClipId("mystery");
            SceneOpener.OpenMainScene();
        }
    }
}