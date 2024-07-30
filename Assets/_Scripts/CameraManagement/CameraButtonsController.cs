using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public enum CameraButtonColor
{
    fine,broken,
}
public class CameraButtonsController : MonoBehaviour, ICameraButtonsController
{
    [SerializeField] private Color _brokenColor;
    [SerializeField] private Color _baseColor;
    
    private readonly List<CameraButton> _buttons = new List<CameraButton>();
    private string _lastButtonId;

    public void AddButton(CameraButton button)
    {
        _buttons.Add(button);
        button.SetColor(_baseColor);
    }

    public void LockButtonById(string id) 
    {
        if(_lastButtonId != null)
        {
            _buttons.Where(button => button.CameraId == _lastButtonId).Single().UnBlockButton();
        }
        
        _buttons.Where(button => button.CameraId == id).Single().BlockButton();
        _lastButtonId = id;
    }

    public void SetColor(string id, CameraButtonColor color)
    {
        var button = _buttons.Single(x => x.CameraId == id);
        switch (color)
        {
            case CameraButtonColor.fine:
                button.SetColor(_baseColor);
                break;
            case CameraButtonColor.broken:
                button.SetColor(_brokenColor);
                break;
        }
    }

}

public interface ICameraButtonsController
{
    public void LockButtonById(string id);
    public void SetColor(string id,CameraButtonColor color);
    public void AddButton(CameraButton button);
}