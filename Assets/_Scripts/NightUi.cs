using DG.Tweening;

using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NightUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _nightText,_timeText;
    [SerializeField] private Image _image;

    [Inject] private AudioPlayer.Pool _audio;
    [Inject] private SurrondAudioPlayer.Pool _3daudio;

    private Sequence _animation; 

    [SerializeField] private float _zoomDur;
    [SerializeField] private float _fadeOutDur;

    public async Task PlayStartAnimation()
    {
        _animation = DOTween.Sequence();
        var col = _image.color;
        col.a = 0;

        _animation.Append(_image.GetComponent<RectTransform>().DOScale(Vector3.one * 1.25f,_zoomDur));
        _animation.Append(_image.DOColor(col, _fadeOutDur));
        _animation.Join(_nightText.DOColor(col, _fadeOutDur));
        _animation.Join( _timeText.DOColor(col, _fadeOutDur));
        _animation.Play();
        
        await Task.Delay((int)_zoomDur * 1110);
        await Task.Delay((int)_fadeOutDur * 1000);
        await Task.Delay(100);

        _animation.Kill();
        gameObject.SetActive(false);
    }
    public async Task PlayEndAnimation()
    {
        _animation = DOTween.Sequence();

        gameObject.SetActive(true);
        _audio.StopAudio(null);
        _3daudio.StopAudio(null);

        var col = _image.color;
        col.a = 1;

        _timeText.text = "6AM by local time";
        _animation.Append(_image.GetComponent<RectTransform>().DOScale(Vector3.one, _zoomDur));
        _animation.Join(_image.DOColor(col, _fadeOutDur ));
        _animation.Join(_nightText.DOColor(Color.white, _fadeOutDur+ _zoomDur));
        _animation.Join(_timeText.DOColor(Color.white, _fadeOutDur + _zoomDur));
        _animation.Play();

        _audio.PlayAudio("nightend",1,255);

        await Task.Delay((int)_zoomDur * 1110);
        await Task.Delay((int)_fadeOutDur * 1000);
        _audio.StopAudioByClipId("nightend");
        await Task.Delay(100);
        _animation.Kill();

    }

    private void OnDestroyed()
    {
        _animation.Kill();
    }
}
