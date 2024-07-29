using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAwakable
{
    public void OnAwake();
}

public interface IClockable
{
    public void Tick();
}


public interface IUpdateable
{
    public void OnUpdate();
}

public interface IFixedUpdateable
{
    public void OnFixedUpdate();
}