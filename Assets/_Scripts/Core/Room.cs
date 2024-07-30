using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room
{
    private string _id;
    public string Id => _id;
    private readonly List<Animatronic> _animatronics = new List<Animatronic>();

    public Room(string id)
    {
        _id = id;
    }
    public void OnGas()
    { 
        foreach(var animatronic in _animatronics)
            animatronic.OnGas();
    }

    public void OnCameraOpened()
    {
        foreach (var animatronic in _animatronics)
            animatronic.OnCameraDetect();
    }
    public void OnCameraLeft()
    {
        foreach (var animatronic in _animatronics)
            animatronic.OnCameraLeave();
    }

    #region AnimatronicManagment

    public void AnimatronicLeave(Animatronic animatronic)
    {
        _animatronics.Remove(animatronic);
    }
    public void AnimatronicEnter(Animatronic animatronic)
    {
        _animatronics.Add(animatronic);
    }
    #endregion
}
