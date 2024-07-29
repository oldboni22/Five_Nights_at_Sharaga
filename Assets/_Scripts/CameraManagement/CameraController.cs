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

    [SerializeField] private GameObject _cameraPanel;
    [SerializeField] private GameObject _actionPanel;

    private CameraButton _lastButton;

    [SerializeField] private string _startCameraId;
    private RoomHandler _curRom;
    private string _lastCameraId;
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
        bool enablePanel = !_cameraPanel.activeInHierarchy;

        _audioPool.PlayAudio(_cameraToggleSound);
        _actionPanel.SetActive(enablePanel);

        if (enablePanel)
        {
            _cameraPanel.SetActive(true);
            OpenCamera(LastCameraId);
        }
        else
        {
            LastCameraId = _curRom.Id;
            _cameraPanel.SetActive(false);
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
            _audioPool.PlayAudio(_blipSound);
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
