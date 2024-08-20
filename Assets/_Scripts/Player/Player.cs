using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

public class Player : IPlayer
{

    [Inject] private AnimatronicSpriteStorage _animatronicSprites;
    [Inject] private IScreamerUI _screamerUI;
    [Inject] private ISceneController _sceneController;

    PlayerSanity _sanity;
    public PlayerSanity Sanity => _sanity; 
    public Player()
    {
        _sanity = new PlayerSanity(this);
    }

    public async Task Death(string id)
    {
        Sprite sprite = _animatronicSprites.GetMemberById(id).Sprite;
        _screamerUI.Screamer(sprite);

        _sceneController.StopUpdate();

        await Task.Delay(2750);

        _screamerUI.GameOver();

        await Task.Delay(2750);

        SceneOpener.OpenMainScene();

    }
}

public interface IPlayer
{
    public Task Death(string id);
    public PlayerSanity Sanity { get; }
}

