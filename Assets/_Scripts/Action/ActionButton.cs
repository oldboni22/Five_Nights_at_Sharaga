using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    private Button _button;
    [SerializeField] private int _cost;
    [SerializeField] private ActionEnum _action;
    [SerializeField] private string _clipId;

    [Inject]
    public void Inject(IActionPointsManager actionPointsManager, AudioPlayer.Pool pool, SoundStorage soundStorage)
    {
        _pool = pool;
        _actionPointsManager = actionPointsManager;
        _actionPointsManager.AddOnPointsEventListener(OnPointsChanged);
        _clip = soundStorage.GetMemberById(_clipId).Clip;

        _button = GetComponent<Button>();
        _button.interactable = false;
    }

    public void Action()
    {
        bool playSound = false;
        switch (_action)
        {
            case ActionEnum.gas:
                playSound = _actionPointsManager.Gas(_cost);
                break;
            case ActionEnum.repair:
                playSound = _actionPointsManager.Repair(_cost); 
                break;
        }

        if(playSound)
            _pool.PlayAudio(_clip,1,120);
    }

    void OnPointsChanged(int points)
    {
        if (points >= _cost)
            _button.interactable = true;
        else
            _button.interactable = false;
    }
}
