using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Bonnie : Animatronic
{
    [SerializeField] private float _patienceRegen;
    [SerializeField] private float _timeToDespawn;
    [SerializeField] private float _patienceLoss;
    [Inject] private IPatienceBar _patienceBar;
    private bool _isPresent = false;
    private float _patience = 1f;
    private float _detectionTime;

    void Spawn()
    {
        _doTurns = false;

        var id = _roomsController.GetRandomRoomId();
        MoveToRoom(id);

        _patienceBar.SetText(_curRoom.FloorString);

        _detectionTime = _timeToDespawn;
        _isPresent = true;
    }
    protected override void Action()
    {
        base.Action();
        Spawn();
    }
    public override void OnUpdate()
    {

        if (_isPresent is false)
        {
            if (_patience < 1f)
            {
                _patience += _patienceRegen * Time.deltaTime;
                if (_patience > 1f)
                    _patience = 1f;

                _patienceBar.OnPatienceChanged(_patience);
            }
            return;
        }

        if (_isObserved is false)
        {
            _patience -= _patienceLoss * Time.deltaTime;
            _patienceBar.OnPatienceChanged(_patience);
            if (_patience <= 0)
            {
                Kill();
            }
        }
        else
        {
            _detectionTime -= Time.deltaTime;
            if (_detectionTime <= 0)
                Despawn();
        }
    }

    private void Despawn()
    {
        _doTurns = true;
        LeaveRoom();
        _patienceBar.SetText();
        _isPresent = false;

    }
}
