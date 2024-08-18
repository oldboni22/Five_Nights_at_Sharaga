using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public interface IScreamerUI
{
    public void Screamer(Sprite sprite);
    public void GameOver();
}
public class ScreamerUi : MonoBehaviour, IScreamerUI
{
    [SerializeField] private Sprite _gameOverSprite;
    [Inject] private AudioPlayer.Pool _audio;
    [Inject] private SurrondAudioPlayer.Pool _audio3D;
    [SerializeField] private Image _image;

    public void GameOver()
    {
        _image.sprite = _gameOverSprite;
    }

    public void Screamer(Sprite sprite)
    {
        _audio.StopAudio(null);
        _audio3D.StopAudio(null);

        _image.gameObject.SetActive(true);
        _image.sprite = sprite;
        _audio.PlayAudio("jumpscare",1,255);
        
    }
}
