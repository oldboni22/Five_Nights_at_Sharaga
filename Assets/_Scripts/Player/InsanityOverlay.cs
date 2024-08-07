using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class InsanityOverlay : MonoBehaviour,IAwakable
{
    [Inject] IPlayer _player;
    [SerializeField] private Image _image;
    [SerializeField] private float _maxOpacity;

    public void OnAwake()
    {
        _player.Sanity.AddOnChangedListener(OnSanityChanged);
    }

    void OnSanityChanged(float val)
    {
        var newCol =_image.color;
        newCol.a = _maxOpacity * (1 - val / 100);
        _image.color = newCol;
    }
}
