using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock : IClock
{
    private readonly List<IClockable> _clockables = new List<IClockable>();

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

}

public interface IClock
{
    public void Tick();
    public void Add(IClockable clockable);
    public void Remove(IClockable clockable);
}
