using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IServiceManager : IUpdateable
{
    public NewDCharge GetCharge();
}
public class ServiceManager : IServiceManager
{

    private NewDCharge _charge;
    public NewDCharge GetCharge()
    {
        _charge ??= new NewDCharge();
        return _charge;
    }

    public void OnUpdate()
    {
        _charge?.OnUpdate();
    }
}
