using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraButton : MonoBehaviour
{
    [Inject] private ICameraControler _cameraController;
    [SerializeField] private string _cameraId;
    [SerializeField] Button _button;
    public string CameraId => _cameraId;
    public void OpenCamera()
    {
        _cameraController.OpenCamera(_cameraId);
    }
    public void BlockButton() => _button.interactable = false;
    public void UnBlockButton() => _button.interactable = true;
}
