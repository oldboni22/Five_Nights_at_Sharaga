using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public interface ICameraControler
{
    public void OpenCamera(string cameraId);
    public RoomHandler CurRoom { get; }
}
public class CameraController : MonoBehaviour, IAwakable, ICameraControler, IUpdateable
{
    [Inject] ICameraButtonsController _buttonsController;
    [Inject] IRoomsController _roomController;
    [Inject] AudioPlayer.Pool _audioPool;

    [SerializeField] private AudioClip _blipSound;
    [SerializeField] private AudioClip _cameraToggleSound;

    [SerializeField] private string _startCameraId;
    private RoomHandler _curRom;
    private string _lastCameraId;

    private bool _enabled = false;
    string LastCameraId
    {
        get
        {
            if (_lastCameraId is null)
            {
                _lastCameraId = _roomController.GetRandomRoomId();
            }

            return _lastCameraId;
        }

        set
        {
            _lastCameraId = value;
        }
    }

    public RoomHandler CurRoom => _curRom;

    private string mainCameraId;

    public void ToggleCamera()
    {
        _enabled = !_enabled;
        _audioPool.PlayAudio(_cameraToggleSound, 1, 100);

        if (_enabled)
        {
            OpenCamera(LastCameraId);
        }
        else
        {
            LastCameraId = _curRom.Id;
            OpenCamera(_startCameraId);
        }

    }

    public void OpenCamera(string cameraId)
    {
        _curRom?.DisconnectFrom();
        _curRom = _roomController.GetRoomById(cameraId);
        _curRom.ConnectTo();
       

        if(cameraId != _startCameraId)
        {
            _buttonsController.LockButtonById(cameraId);
            _audioPool.PlayAudio(_blipSound, 1, 100);
        }
            
    }

    public void OnAwake()
    {
        OpenCamera(_startCameraId);
    }

    public void OnUpdate()
    {
        if(_curRom.Id != _startCameraId)
            _curRom.OnUpdate();
    }
}
