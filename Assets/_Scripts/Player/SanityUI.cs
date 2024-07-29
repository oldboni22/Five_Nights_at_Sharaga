using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

public class SanityUI : MonoBehaviour, IUpdateable
{
    [Inject] private IPlayer  _player;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    public void OnUpdate()
    {
        if(_slider.value != _player.Sanity.CurSanity)
        {
            _slider.value = Mathf.Lerp(_slider.value, _player.Sanity.CurSanity, Time.deltaTime + 0.015f);
            _text.text = $"Sanity - {_player.Sanity.CurSanity}%";
        }
    }
}
