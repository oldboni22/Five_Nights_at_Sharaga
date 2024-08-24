using DG.Tweening;

using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NightStartUi : MonoBehaviour
{
    [SerializeField] private TMP_Text _nightText,_timeText;
    [SerializeField] private Image _image;


    [SerializeField] private float _zoomDur;
    [SerializeField] private float _fadeOutDur;

    public async Task PlayAnimation()
    {
        _image.GetComponent<RectTransform>().DOScale(Vector3.one * 1.25f,_zoomDur);
        await Task.Delay((int)_zoomDur * 1000);

        var col = _image.color;
        col.a = 0;
        _image.DOColor(col, _fadeOutDur);
        _nightText.DOColor(col, _fadeOutDur);
        _timeText.DOColor(col, _fadeOutDur);
        await Task.Delay((int)_fadeOutDur * 1000);

        Destroy(gameObject);
    }
    private void OnDestroy()
    {
        _image.DOKill();
        _nightText.DOKill();
        _timeText.DOKill();
    }
}
