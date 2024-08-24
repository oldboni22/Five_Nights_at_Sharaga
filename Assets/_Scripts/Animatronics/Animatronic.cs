using System;

using UnityEngine;

using Zenject;

public abstract class Animatronic : MonoBehaviour, IUpdateable
{
    #region paremeters

    [SerializeField] protected ushort _ticksToAction;
    [SerializeField] protected ushort _failedTicksLimit;
    protected ushort _succeded;
    protected bool _isObserved = false;


    protected bool _doTurns = true;
    protected bool _countSucces = true;

    [Inject] protected IRoomsController _roomsController;
    [Inject] protected IPlayer _player;

    [SerializeField] protected string _id;
    public string Id => _id;
    protected int _failedTicks;
    private ushort _difficultyLevel;
    protected RoomHandler _curRoom;

    
    public void SetDifficulty(ushort difficulty)
    {
        _difficultyLevel = difficulty;
    }

    #endregion

    #region Turn

    public void Turn(ushort random)
    {

        if (_doTurns is false)
            return;

        Debug.Log(_id + random + " - Random");
        if(_difficultyLevel > random)
            OnSucceedTurn();
        else
            OnFailedTurn();

    }
    protected virtual void OnFailedTurn()
    {
        _failedTicks++;
        Debug.Log($"{_id} - Turn failed, failed turns - {_failedTicks}");

        if(_failedTicks >= _failedTicksLimit)
        {
            OnFailedLimitReached();
        }
    }
    protected virtual void OnSucceedTurn()
    {
        if (_countSucces is false)
            return;

        _succeded++;
        if (_succeded >= _ticksToAction)
            Action();
        Debug.Log($"{_id} - Turn succeed! - " + _succeded + " succeded");
    }
    protected virtual void OnFailedLimitReached()
    {
        Debug.Log(_id + "Failed linit reached");
        OnSucceedTurn();
        _failedTicks = 0;
    }
    protected virtual void Action() 
    { 
        Debug.Log(_id + "Action !");
        _failedTicks = 0;
        _succeded = 0;
    }
    #endregion

    #region RoomManagment

    protected void MoveToRoom(string roomId)
    {
        Debug.Log(_id + " Moved to " + roomId);
        _curRoom?.AnimatronicLeave(this);
        _curRoom = _roomsController.GetRoomById(roomId);
        _curRoom.AnimatronicEnter(this);
    }

    protected void LeaveRoom() 
    {
        Debug.Log(_id + " left " + _curRoom.Id);
        _curRoom?.AnimatronicLeave(this);
        _curRoom = null;
    }
    
    #endregion

    #region OnEvents
    public virtual void OnGas()
    {
        Debug.Log($"{gameObject.name} - gased");
    }
    public virtual void OnCameraDetect()
    {
        Debug.Log($"{gameObject.name} - detected");
        _isObserved = true;
    }
    public virtual void OnCameraLeave()
    {
        Debug.Log($"{gameObject.name} - left camera vision");
        _isObserved = false;
    }

    public virtual void OnUpdate() { }
    #endregion

    protected void Kill()
    {
        _player.Death(_id);
    }
}
