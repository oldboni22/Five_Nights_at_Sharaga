using System;
using UnityEngine;


public interface INightTimer : IFixedUpdateable
{
    public event Action<ushort> OnHourPass;
}
public class NightTimer : INightTimer
{
    public event Action<ushort> OnHourPass;

    const ushort Seconds = 60;

    private  float _timer = Seconds;
    private ushort _hours = 0;

    public void OnFixedUpdate()
    {
        _timer -= Time.fixedDeltaTime;
        if(_timer < 0)
        {
            _hours++;
            OnHourPass?.Invoke(_hours);
            _timer = Seconds;
        }
    }
}
