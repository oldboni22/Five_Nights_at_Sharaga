using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class NewD : Animatronic
{

    [SerializeField] private float _drain;
    [SerializeField] private float _chargingDrain;
    [SerializeField] private float _tickRateChange;
    
    private string _curName;

    private bool _isPresent = false;
    private bool _isPlaying = false;

    private IClock _clock;
    private AudioPlayer.Pool _audioPool;
    private NewDCharge _newd;
    private NewdStorage _newdStorage;

    [Inject]
    public void Inject(IServiceManager service, AudioPlayer.Pool pool, NewdStorage newdStorage,IClock clock )
    {
        _audioPool = pool;
        _newd = service.GetCharge();
        _newdStorage = newdStorage;
        _clock = clock;
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

        _clock.ChangeClockMod(-_tickRateChange);
        _doTurns = true;

        _audioPool.StopAudio(_curName);

        
        _isPlaying = false;
    }
    public override void OnUpdate()
    {
        base.OnUpdate();
        if (_isPresent && _newd.IsStalled is false )
            _newd.Drain(_drain);
        else if(_isPlaying && _newd.IsCharged)
            _newd.Drain(_chargingDrain);
    }

    void PlayRandomClip()
    {
        _isPresent = false;
        _clock.ChangeClockMod(_tickRateChange);
        var clip = _newdStorage.GetRandom();
        _curName = clip.name;
        _audioPool.LoopAudio(clip, 1, 255);
    }
}
