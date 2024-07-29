using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GasButton : MonoBehaviour
{
    private AudioPlayer.Pool _pool;
    private IActionPointsManager _actionPointsManager;
    private ICameraControler _cameraController;
    private AudioClip _gasClip;

    [Inject]
    public void Inject(IActionPointsManager actionPointsManager, ICameraControler cameraController,AudioPlayer.Pool pool, SoundStorage soundStorage)
    {
        _pool = pool;
        _actionPointsManager = actionPointsManager;
        _cameraController = cameraController;
        _gasClip = soundStorage.GetMemberById("gas").Clip;
    }

    public void Gas()
    {
        if (_actionPointsManager.SpendPoints(1))
        {
            _cameraController.CurRoom.GasRoom();
            _pool.PlayAudio(_gasClip);
        }
    }
}
