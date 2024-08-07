using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Player : IPlayer
{
    PlayerSanity _sanity;
    public PlayerSanity Sanity => _sanity; 
    public Player()
    {
        _sanity = new PlayerSanity();
    }

}

public interface IPlayer
{
    public PlayerSanity Sanity { get; }
}

