using ModestTree.Util;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
public interface IActionPointsManager : IFixedUpdateable
{
    public ushort Points { get; }
    public void DisableAction(ActionEnum action, float dur);
    public void AddPoints(ushort points);
    public void AddOnChargeEventListener(Action<float> @delegate);
    public void AddOnPointsEventListener(Action<int> @delegate);
    public void AddOnDisableCallListener(Action<ActionEnum, float> @delegate);
    public bool Gas(ushort cost);
    public bool Repair(ushort cost);
    public bool Pills(ushort cost);

}

public class ActionPointsManager : IActionPointsManager
{
    [Inject] IPlayer _player;

    private ushort _pillsUsageCount = 0;

    private event Action<ActionEnum,float> _onDisableCall; 
    private event Action<float> _onActionChargeChanged;
    private event Action<int> _onActionPointsChanged;
    private ICameraControler _cameraControler;

    AudioClip _sound;
    private readonly string _soundId = "AP1";
    private AudioPlayer.Pool _pool;

    private readonly ushort _maxPoints = 4;
    private ushort _points;

    private float _pointCharge;
    private readonly float _pointGain = 0.125f;

    public ushort Points => _points;



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

    bool SpendPoints(ushort points)
    {
        if(_points - points >= 0)
        {
            _points -= points;
            _onActionPointsChanged?.Invoke(_points);

            return true;
        }


        return false;
    }

    public bool Gas(ushort cost)
    {
        if (SpendPoints(cost))
        {
            _cameraControler.CurRoom.GasRoom();
            return true;
        }
        return false;
    }

    public bool Repair(ushort cost)
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
    public void AddOnDisableCallListener(Action<ActionEnum,float> @delegate)
    {
        _onDisableCall += @delegate;
    }

    public bool Pills(ushort cost)
    {
        if (SpendPoints(cost))
        {
            _player.Sanity.CurSanity += 20f - (_pillsUsageCount * 2.5f);
            _pillsUsageCount++;

            return true;
        }
        return false;
    }

    public void AddPoints(ushort points)
    {
        _points += points;
        if(_points > _maxPoints)
            _points = _maxPoints;

        _pool.PlayAudio(_sound, 1, 100);
        _onActionPointsChanged?.Invoke(_points);
    }

    public void DisableAction(ActionEnum action,float dur)
    {
        _onDisableCall.Invoke(action,dur);
    }
}

