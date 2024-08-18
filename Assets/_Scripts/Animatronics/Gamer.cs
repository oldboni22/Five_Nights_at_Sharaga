using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class Gamer : Animatronic
{

    [Inject] private IActionPointsManager _actionPointsManager;

    [SerializeField] private RectTransform _gamePanel;
    [SerializeField] private Button _button;

    [SerializeField] private ushort _clicksNeeded;
    [SerializeField] private float _secondsToPass;
    [SerializeField] private float _blackOutDur;

    private ushort _triggerCount;
    private ushort _clicks;
    private float _timer;

    protected override void Action()
    {
        base.Action();
        Spawn();
    }

    private void Start()
    {
        _button.onClick.AddListener(Click);
    }

    void Spawn()
    {
        _gamePanel.DOScale(1, .1f);

        _timer = _secondsToPass;
        _clicks = 0;

        _doTurns = false;
    }

    public override void OnUpdate()
    {
        if(_doTurns is false)
        {
            _timer -= Time.deltaTime;
            if(_timer <= 0)
            {
                Fail();
            }
        }    
    }

    void Click()
    {
        _clicks++;
        if (_clicks == _clicksNeeded + _triggerCount)
            Succes();
    }

    void Fail()
    {
        Despawn();
        _actionPointsManager.DisableAction(ActionEnum.repair, _blackOutDur);
        _roomsController.BlackOut(_blackOutDur);
    }

    void Succes()
    {
        Despawn();
        _actionPointsManager.AddPoints(1);
    }

    private void Despawn()
    {
        _doTurns = true;

        _gamePanel.DOKill();
        _gamePanel.DOScale(0, .1f);
        _triggerCount++;
    }
}
