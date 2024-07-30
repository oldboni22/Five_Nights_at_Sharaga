using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;
using Zenject;

public abstract class Animatronic : MonoBehaviour
{
    #region paremeters

    [Inject] private IRoomsController _roomsController;
    [SerializeField] private string _id;
    public string Id => _id;
    protected int _failedTicks;
    protected int _failedTicksTreshold;
    protected int _difficultyLevel;
    protected RoomHandler _curRoom;

    
    public virtual void SetParameters(AnimatronicDifficulty difficulty)
    {
        _failedTicksTreshold = difficulty.FailedTicksTreshold;
        _difficultyLevel = difficulty.DifficultyLevel;
    }

    #endregion

    #region Turn

    public void Turn(int random)
    {
        if(_difficultyLevel > random)
            OnSucceedTurn();
        else
            OnFailedTurn();

        Debug.Log($"{gameObject.name} - Turn");
    }
    protected virtual void OnFailedTurn()
    {
        _failedTicks++;
        Debug.Log($"{gameObject.name} - Turn failed, failed turns - {_failedTicks}");

        if(_failedTicks == _failedTicksTreshold)
        {
            Debug.Log($"{gameObject.name} - Fail treshhold is reached, succes triggered anyway...");
            OnSucceedTurn();
            _failedTicks = 0;
        }
    }
    protected virtual void OnSucceedTurn()
    {
        Debug.Log($"{gameObject.name} - Turn succeed!");
    }

    #endregion

    #region RoomManagment

    protected void MoveToRoom(string roomId)
    {
        _curRoom?.AnimatronicLeave(this);
        _curRoom = _roomsController.GetRoomById(roomId);
        _curRoom.AnimatronicEnter(this);
    }

    protected void LeaveRoom() 
    {
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
    }
    public virtual void OnCameraLeave()
    {
        Debug.Log($"{gameObject.name} - detected");
    }
    #endregion


}
