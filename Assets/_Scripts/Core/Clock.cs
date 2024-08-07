using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Clock : IClock
{
    [Inject] IPlayer _player;

    private readonly List<IClockable> _clockables = new List<IClockable>();

    private float _timer = 0;
    private float _timeToTick;
    private float _sanityVar;

    public void Tick()
    {
        Debug.Log("1 tick");
        foreach(IClockable clockable in _clockables)
        {
            clockable.Tick();
        }
    }

    public void Add(IClockable clockable) => _clockables.Add(clockable);
    public void Remove(IClockable clockable) => _clockables.Remove(clockable);

    public void OnUpdate()
    {
        _timer += Time.deltaTime;
        if(_timer >= _timeToTick - _sanityVar)
        {
            Tick();
            _timer = 0;
            Debug.Log(_timeToTick - _sanityVar + " - Time to next tick");

        }
        
    }

    public void SetTimeToTick(float time)
    {
        _timeToTick = time;
    }

    public void OnSanityChanged(float val)
    {
        float GetMultiplyer(float sanity)
        {
            if (sanity >= 75)
                return .075f;
            else if (sanity >= 50)
                return .1f;
            else if (sanity >= 25)
                return .125f;
            else return .15f;
        }

        if (val == 0)
        {

            _sanityVar = 3f;
        }
        else
        {
            _sanityVar = 1 - val / 100;

            _sanityVar += ((Int16)(100 - val) / 10) * GetMultiplyer(val);
        }
    }
    
    public void OnAwake()
    {
        _player.Sanity.AddOnChangedListener(OnSanityChanged);
    }
}

public interface IClock : IUpdateable, IAwakable
{
    public void Tick();
    public void Add(IClockable clockable);
    public void Remove(IClockable clockable);
    public void SetTimeToTick(float time);

}
