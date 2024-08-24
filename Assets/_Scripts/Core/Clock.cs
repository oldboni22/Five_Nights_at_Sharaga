using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class Clock : IClock
{

    private readonly List<IClockable> _clockables = new List<IClockable>();

    private float _timer = 0;
    private float _timeToTick;

    private float _timeMod = 0;
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
        if(_timer >= _timeToTick - _timeMod - _sanityVar)
        {
            Tick();
            _timer = 0;
            Debug.Log(_timeToTick - _timeMod - _sanityVar + " - Time to next tick");

        }
        
    }

    public void SetTimeToTick(float time)
    {
        _timeToTick = time;
    }


    public void ChangeClockMod(float val)
    {
        _timeMod += val;
    }

    public void SetSanityVar(float val)
    {
        _sanityVar = val;
    }
}

public interface IClock : IUpdateable
{
    public void Tick();
    public void Add(IClockable clockable);
    public void Remove(IClockable clockable);
    public void SetTimeToTick(float time);
    public void ChangeClockMod(float val);
    public void SetSanityVar(float val);

}
