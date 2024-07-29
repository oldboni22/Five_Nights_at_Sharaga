using ModestTree.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;


public class ActionPointsManager : IActionPointsManager
{
    private event Action<float,int> OnActionPointsChanged;

    AudioClip _sound;
    private readonly string _soundId = "AP1";
    private AudioPlayer.Pool _pool;

    private readonly int _maxPoints = 4;
    private int _points;

    private float _pointCharge;
    private readonly float _pointGain = 0.16f;


    [Inject] 
    public void Inject(SoundStorage storage, AudioPlayer.Pool audioPool)
    {
        _pool = audioPool;
        _sound = storage.GetMemberById(_soundId).Clip;
    }

    void ChargePoint()
    {
        if(_points == _maxPoints) return;

        _pointCharge += _pointGain * Time.fixedDeltaTime;
        if(_pointCharge >= 1)
        {
            _pointCharge = 0;
            _points++;
            _pool.PlayAudio(_sound);
        }
        
        OnActionPointsChanged?.Invoke(_pointCharge,_points);
    }

    public void OnFixedUpdate()
    {
        ChargePoint();
    }

    public void AddEventListener(Action<float, int> @delegate)
    {
        OnActionPointsChanged += @delegate;
    }

    public bool SpendPoints(int points)
    {
        if(_points - points >= 0)
        {
            _points -= points;
            OnActionPointsChanged?.Invoke(_pointCharge, _points);

            return true;
        }


        return false;
    }
}

public interface IActionPointsManager : IFixedUpdateable
{
    public void AddEventListener(Action<float,int> @delegate);
    public bool SpendPoints(int points);
   
}