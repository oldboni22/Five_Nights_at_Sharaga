
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IOpenCameraButton
{
    public void AddOnClickListener(Action action);
    public void DisableButton(float dur);
}

public class OpenCameraButton : MonoBehaviour, IOpenCameraButton, IAwakable, IUpdateable
{
    private bool _enabled;
    private float _timer;

    [SerializeField] private Button _button;
    public void AddOnClickListener(Action action) => _button.onClick.AddListener(() => action());

    public void DisableButton(float dur)
    {
        _timer = dur;
        _button.interactable = false;

        if (_enabled)
            _button.onClick.Invoke();
    }

    public void OnAwake()
    {
        _button.onClick.AddListener(() => _enabled = !_enabled);
    }

    public void OnUpdate()
    {
        if(_timer != 0)
        {
            _timer -= Time.deltaTime;
            if(_timer < 0)
            {
                _timer = 0;
                _button.interactable = true;
            }
        }
    }

}
