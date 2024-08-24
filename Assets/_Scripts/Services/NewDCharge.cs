using System;
using System.Collections.Generic;
using UnityEngine;

public class NewDCharge : IUpdateable
{
    public bool IsCharged => _isCharged;
    public bool IsStalled => _isStalled;

    private event Action _onChargeEnded;
    private event Action<float> _onChargeChanged;
    private event Action _onFullCharged;

    private float _stalled;
    private float _charge;
    private bool _isCharged = false;
    private bool _isStalled = false;
    private float _chargeAmount = 1;

    public void AddOnChargeEndedListener(Action action) => _onChargeEnded += action;
    public void AddOnFullChargedListener(Action action) => _onFullCharged += action;
    public void AddOnChargeChangedListener(Action<float> action) => _onChargeChanged += action;

    public void Charge(float val)
    {
        _chargeAmount += val * Time.deltaTime;
        if (_chargeAmount >= 1f)
        {
            _chargeAmount = 1f;
            _onFullCharged?.Invoke();
            return;
        }

        _stalled = 1.25f;
        _charge = .2f;

        _isStalled = true;
        _isCharged = true;


        _onChargeChanged?.Invoke(_chargeAmount);
    }
    public void Drain(float val)
    {
        if (_chargeAmount == 0)
            return;

        _chargeAmount -= val * Time.deltaTime;
        _onChargeChanged?.Invoke(_chargeAmount);
        if (_chargeAmount <= 0)
        {
            _chargeAmount = 0;
            _onChargeEnded?.Invoke();
        }
    }
    public void OnUpdate()
    {
        if (_isCharged)
        {
            _charge -= Time.deltaTime;
            if(_charge < 0)
            {
                _charge = 0;
                _isCharged = false;
            }
        }
        if (_isStalled)
        {
            _stalled -= Time.deltaTime;
            if (_stalled < 0)
            {
                _stalled = 0;
                _isStalled = false;
            }

        }
    }
}
