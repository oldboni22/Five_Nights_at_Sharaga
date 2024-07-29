using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RepairButton : MonoBehaviour
{
    [Inject] private IActionPointsManager _actionPointsManager;
    [Inject] private ICameraControler _cameraController;


    public void Repair()
    {
        if (_actionPointsManager.SpendPoints(2))
        {
            _cameraController.CurRoom.EnableCamera();
        }
    }
}
