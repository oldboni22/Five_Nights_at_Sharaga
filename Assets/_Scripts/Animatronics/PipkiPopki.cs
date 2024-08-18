using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PipkiPopki : Animatronic
{
    [SerializeField] private float _sanityReduction;


    [SerializeField] private float _volume;

    [Inject] private PipkiPopkiStorage _pipki;
    [Inject] private SurrondAudioPlayer.Pool _3dAudioPool;
    [Inject] private AudioPlayer.Pool _audioPool;
    
    private bool _isPlaying = false;
    private string _curId;

    [Inject]
    public void Inject( PipkiPopkiStorage pipki, SurrondAudioPlayer.Pool audioPool3d, AudioPlayer.Pool audioPool)
    {
        _pipki = pipki;
        _3dAudioPool = audioPool3d;
        _audioPool = audioPool;
    }

    protected override void Action()
    {
        base.Action();
        Spawn();
    }
    protected override void OnFailedLimitReached()
    {
        if(_isPlaying)
            Despawn();
        else
            base.OnFailedLimitReached();
    }
    public override void OnGas()
    {
        base.OnGas();
        Despawn();
    }

    void Spawn()
    {
        _countSucces = false;
        _isPlaying = true;
        _audioPool.PlayAudio(_pipki.Spawn,1,250);

        MoveToRoom(_roomsController.GetRandomRoomId()) ;
        
        var clip = _pipki.GetRandom();
        _curId = clip.name;

        _3dAudioPool.LoopAudio(clip,_curRoom.transform.position,_volume,145);
    }

    public override void OnUpdate()
    {
        if(_isPlaying)
            _player.Sanity.CurSanity -= _sanityReduction * Time.deltaTime;
    }

    void Despawn()
    {
        _countSucces = true;
        LeaveRoom();
        _3dAudioPool.StopAudio(_curId);
        _audioPool.PlayAudio(_pipki.Despawn, 1, 250 );
        _isPlaying = false;
    }
}
