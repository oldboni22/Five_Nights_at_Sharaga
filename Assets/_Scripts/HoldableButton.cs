using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoldableButton : Button
{
    private event Action _onHeld;
    private bool _isHeld = false;

    public void AddOnHoldListener(Action action) => _onHeld += action; 

    private void Update()
    {
        if(_isHeld)
            _onHeld?.Invoke();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        _isHeld = true;
    }
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        _isHeld = false;
    }
}
