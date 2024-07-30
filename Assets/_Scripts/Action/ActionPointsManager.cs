using ModestTree.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;


public class ActionPointsManager : IActionPointsManager
{
    private event Action<float,int> OnActionPointsChanged;
    private ICameraControler _cameraControler;

    AudioClip _sound;
    private readonly string _soundId = "AP1";
    private AudioPlayer.Pool _pool;

    private readonly int _maxPoints = 4;
    private int _points;

    private float _pointCharge;
    private readonly float _pointGain = 0.125f;


    [Inject] 
    public void Inject(SoundStorage storage, AudioPlayer.Pool audioPool, ICameraControler cameraControler)
    {
        _pool = audioPool;
        _sound = storage.GetMemberById(_soundId).Clip;
        _cameraControler = cameraControler;
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

    bool SpendPoints(int points)
    {
        if(_points - points >= 0)
        {
            _points -= points;
            OnActionPointsChanged?.Invoke(_pointCharge, _points);

            return true;
        }


        return false;
    }

    public bool Gas()
    {
        if (SpendPoints(1))
        {
            _cameraControler.CurRoom.GasRoom();
            return true;
        }
        return false;
    }

    public bool Repair()
    {
        if (SpendPoints(2))
        {
            _cameraControler.CurRoom.EnableCamera();
            return true;
        }
        return false;
    }
}

public interface IActionPointsManager : IFixedUpdateable
{
    public void AddEventListener(Action<float,int> @delegate);
    public bool Gas();
    public bool Repair();
   
}