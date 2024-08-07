using ModestTree.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public interface IActionPointsManager : IFixedUpdateable
{
    public void AddOnChargeEventListener(Action<float> @delegate);
    public void AddOnPointsEventListener(Action<int> @delegate);
    public bool Gas(int cost);
    public bool Repair(int cost);

}

public class ActionPointsManager : IActionPointsManager
{
    private event Action<float> _onActionChargeChanged;
    private event Action<int> _onActionPointsChanged;
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
            _pool.PlayAudio(_sound,1,100);
            _onActionPointsChanged?.Invoke(_points);
        }
        
        _onActionChargeChanged?.Invoke(_pointCharge);
    }

    public void OnFixedUpdate()
    {
        ChargePoint();
    }

    bool SpendPoints(int points)
    {
        if(_points - points >= 0)
        {
            _points -= points;
            _onActionPointsChanged?.Invoke(_points);

            return true;
        }


        return false;
    }

    public bool Gas(int cost)
    {
        if (SpendPoints(cost))
        {
            _cameraControler.CurRoom.GasRoom();
            return true;
        }
        return false;
    }

    public bool Repair(int cost)
    {
        if (SpendPoints(cost))
        {
            _cameraControler.CurRoom.EnableCamera();
            return true;
        }
        return false;
    }

    public void AddOnChargeEventListener(Action<float> @delegate)
    {
        _onActionChargeChanged += @delegate;
    }

    public void AddOnPointsEventListener(Action<int> @delegate)
    {
        _onActionPointsChanged += @delegate;
    }
}

