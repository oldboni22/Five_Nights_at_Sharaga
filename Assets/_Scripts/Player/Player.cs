using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;
using Sanity;

public class Player : IPlayer
{

    [Inject] private IScreamerUiController _screamerUi;
    [Inject] private ISceneController _sceneController;
    [Inject] private IClock _clock;

    PlayerSanity _sanity;
    public PlayerSanity Sanity => _sanity; 
    public Player()
    {
        _sanity = new PlayerSanity(this);
    }

    public void Death(string id)
    {
        _sceneController.StopUpdate();
        _screamerUi.Jumpscare(id, true);
    }

    public void OnAwake()
    {
        _sanity._clock = _clock;
    }
}

public interface IPlayer : IAwakable
{
    public void Death(string id);
    public PlayerSanity Sanity { get; }
}

