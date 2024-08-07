using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ServicesOpener : MonoBehaviour
{
    private bool _opened = false;
    [Inject] private AudioPlayer.Pool _pool;
    

    [SerializeField] private float _animTime;


    [SerializeField] private RectTransform _transform;
    [SerializeField] private Button _camButton;


    public void OnClick()
    {
        if(_opened)
            ClosePanel();
        else
            OpenPanel();

        _pool.PlayAudio("service_open",1,250);
    }

    void OpenPanel()
    {
        _camButton.interactable = false;
        _opened = true;
        _transform.DOKill();
        _transform.DOScaleX(1, _animTime);
    }

    void ClosePanel()
    {
        _camButton.interactable = true;
        _opened = false;
        _transform.DOKill();
        _transform.DOScaleX(0, _animTime);
    }
}
