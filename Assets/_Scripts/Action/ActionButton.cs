using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;



public enum ActionEnum
{
    gas, repair, pills,
}
public class ActionButton : MonoBehaviour
{

    [SerializeField] private MonoBehaviour _coroutineObj;

    private bool _enabled = true;

    private AudioPlayer.Pool _pool;
    private IActionPointsManager _actionPointsManager;
    private AudioClip _clip;

    private Button _button;
    [SerializeField] private ushort _cost;
    [SerializeField] private ActionEnum _action;
    [SerializeField] private string _clipId;

    [Inject]
    public void Inject(IActionPointsManager actionPointsManager, AudioPlayer.Pool pool, SoundStorage soundStorage)
    {
        _pool = pool;
        _actionPointsManager = actionPointsManager;
        _actionPointsManager.AddOnPointsEventListener(OnPointsChanged);
        _actionPointsManager.AddOnDisableCallListener(OnDisableCall);
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
            case ActionEnum.pills:
                playSound = _actionPointsManager.Pills(_cost);
                _cost++;
                if (playSound)
                    GetComponentInChildren<TMP_Text>().text = $"PILLS - {_cost}";
                break;
        }

        if (playSound)
            _pool.PlayAudio(_clip, 1, 120);
    }
    void OnDisableCall(ActionEnum action, float dur)
    {
        if (action == _action)
            _coroutineObj.StartCoroutine(DisableButton(dur));

    }
    void OnPointsChanged(int points)
    {
        if (_enabled is false)
            return;

        if (points >= _cost)
            _button.interactable = true;
        else
            _button.interactable = false;
    }

    IEnumerator DisableButton(float dur)
    {
        _enabled = false;
        _button.interactable = false;
        yield return new WaitForSeconds(dur);

        if (_actionPointsManager.Points >= _cost)
            _button.interactable = true;

        _enabled = true;
    }
}
