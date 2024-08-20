using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public interface IVentButton
{
    public void AddOnClick(Action action);
}
public class VentButton : MonoBehaviour, IVentButton
{
    [SerializeField] private Button _button;
    [SerializeField] private float _rebootDur;

    private void Start()
    {
        _button.onClick.AddListener(() => StartCoroutine(RebootButton()));
    }
    public void AddOnClick(Action action)
    {
        _button.onClick.AddListener(() => action());
    }

    IEnumerator RebootButton()
    {
        _button.interactable = false;
        yield return new WaitForSeconds(_rebootDur);
        _button.interactable = true;
    }
}
