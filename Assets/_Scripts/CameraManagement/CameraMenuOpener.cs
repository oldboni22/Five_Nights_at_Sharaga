using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CameraMenuOpener : MonoBehaviour, IAwakable
{

    [SerializeField] private float _duration;
    [SerializeField] private float _targetX;
    private float _startX;

    private bool _enabled = false;

    [SerializeField] private GameObject _blocker;
    [SerializeField] private RectTransform _transform;

    [Inject]
    public void Inject(IOpenCameraButton button) => button.AddOnClickListener(OnClick);


    public void OnAwake()
    {
        _startX = _transform.localPosition.x;
        Debug.Log(_startX);
    }

    public void OnClick()
    {
        if (_enabled is false)
        {
            OpenMenu();
        }
        else
        {
            CloseMenu();
        }
    }

    private void OpenMenu()
    {
        _blocker.SetActive(true);

        _enabled = true;
        _transform.DOKill();
        _transform.DOAnchorPosX(_targetX, _duration).OnComplete(() => { _blocker.SetActive(false); });
    }

    private void CloseMenu()
    {
        _blocker.SetActive(true);

        _enabled = false;
        _transform.DOKill();
        _transform.DOAnchorPosX(_startX, _duration);
    }
}
