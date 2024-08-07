using System;
using System.Collections.Generic;
using UnityEngine;

public class NewDCharge : IUpdateable
{
    public bool IsCharged => _isCharged;

    private event Action _onChargeEnded;
    private event Action<float> _onChargeChanged;
    private event Action _onFullCharged;


    private float _lastTimeCharged;
    private bool _isCharged = false;
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

        _lastTimeCharged = .75f;
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
            _lastTimeCharged -= Time.deltaTime;
            if (_lastTimeCharged <= 0)
            {
                _lastTimeCharged = 0;
                _isCharged = false;
            }

        }
    }
}
