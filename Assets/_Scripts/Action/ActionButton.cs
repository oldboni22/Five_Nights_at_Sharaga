using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public enum ActionEnum
{
    gas,repair,
}
public class ActionButton : MonoBehaviour
{
    private AudioPlayer.Pool _pool;
    private IActionPointsManager _actionPointsManager;
    private AudioClip _clip;

    [SerializeField] private ActionEnum _action;
    [SerializeField] private string _clipId;

    [Inject]
    public void Inject(IActionPointsManager actionPointsManager, AudioPlayer.Pool pool, SoundStorage soundStorage)
    {
        _pool = pool;
        _actionPointsManager = actionPointsManager;
        _clip = soundStorage.GetMemberById(_clipId).Clip;
    }
    public void Action()
    {
        bool playSound = false;
        switch (_action)
        {
            case ActionEnum.gas:
                playSound = _actionPointsManager.Gas();
                break;
            case ActionEnum.repair:
                playSound = _actionPointsManager.Repair(); 
                break;
        }

        if(playSound)
            _pool.PlayAudio(_clip);
    }
}
