using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class CameraButton : MonoBehaviour
{
    private ICameraControler _cameraController;
    [SerializeField] private string _cameraId;
    [SerializeField] Button _button;
    public string CameraId => _cameraId;

    [Inject]
    public void Inject(ICameraControler cameraController, ICameraButtonsController cameraButtonsController)
    {
        cameraButtonsController.AddButton(this);
        _cameraController = cameraController;
    }
    public void OpenCamera()
    {
        _cameraController.OpenCamera(_cameraId);
    }
    public void BlockButton() => _button.interactable = false;
    public void UnBlockButton() => _button.interactable = true;
    public void SetColor(Color color) => _button.image.color = color;
}
