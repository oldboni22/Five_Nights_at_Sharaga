using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NewD : Animatronic
{

    [SerializeField] private float _drain;
    [SerializeField] private float _chargingDrain;

    private string _curName;

    private bool _isPresent = false;
    private bool _isPlaying = false;

    private AudioPlayer.Pool _audioPool;
    private NewDCharge _newd;
    private NewdStorage _newdStorage;

    [Inject]
    public void Inject(IServiceManager service, AudioPlayer.Pool pool, NewdStorage newdStorage)
    {
        _audioPool = pool;
        _newd = service.GetCharge();
        _newdStorage = newdStorage;
    }

    private void Start()
    {
        _newd.AddOnChargeEndedListener(() =>
        {
            if (_isPlaying is false)
                PlayRandomClip();
            _isPlaying = true; }
        );
        _newd.AddOnFullChargedListener(Despawn);
    }

    protected override void Action()
    {
        base.Action();
        Spawn();
    }

    void Spawn()
    {
        _doTurns = false;
        _isPresent = true;
    }
    void Despawn()
    {
        if (_isPlaying is false)
            return;

        _doTurns = true;

        _audioPool.StopAudio(_curName);

        _isPresent = false;
        _isPlaying = false;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_isPresent && _newd.IsCharged is false)
            _newd.Drain(_drain);
        else if(_isPlaying && _newd.IsCharged)
            _newd.Drain(_chargingDrain);
    }

    void PlayRandomClip()
    {
        var clip = _newdStorage.GetRandom();
        _curName = clip.name;
        _audioPool.LoopAudio(clip, 1, 255);
    }
}
