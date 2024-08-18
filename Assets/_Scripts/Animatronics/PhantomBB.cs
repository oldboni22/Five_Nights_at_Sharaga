using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PhantomBB : Animatronic
{
    [SerializeField] private float _sanityeReduction;
    [SerializeField] private ushort _failedToLeave;
    [Inject] OverlayImage.Pool _overlayPool;
    [SerializeField] private Sprite _sprite;


    private bool _isPresent = false;

    protected override void OnFailedLimitReached()
    {
        if(_isPresent)
            Despawn();
        else base.OnFailedLimitReached();
    }
    protected override void Action()
    {
        base.Action();
        Spawn();
    }
    void Spawn()
    {
        _countSucces = false;
        MoveToRoom(_roomsController.GetRandomRoomId());
        _isPresent = true;
        _overlayPool.SetOverlay(new SetOverlayImageParams
        {
            id = _id,
            prio = 110,
            room = _curRoom,
            sprite = _sprite,
        }) ;
    }
    void Despawn()
    {
        _countSucces = true;
        _isPresent = false;
        _curRoom.RemoveOverlayImage(_id);
        LeaveRoom();
    }

    public override void OnUpdate()
    {
        if (_isObserved)
            _player.Sanity.CurSanity -= _sanityeReduction * Time.deltaTime;
    }
    public override void OnGas()
    {
        Despawn();
    }

}
