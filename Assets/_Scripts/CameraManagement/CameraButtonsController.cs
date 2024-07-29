using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraButtonsController : MonoBehaviour, ICameraButtonsController
{
    
    [SerializeField] private List<CameraButton> _buttons = new List<CameraButton>();
    private string _lastButtonId;
    public void LockButtonById(string id) 
    {
        if(_lastButtonId != null)
        {
            _buttons.Where(button => button.CameraId == _lastButtonId).Single().UnBlockButton();
        }
        
        _buttons.Where(button => button.CameraId == id).Single().BlockButton();
        _lastButtonId = id;
    } 

}

public interface ICameraButtonsController
{
    public void LockButtonById(string id);
}