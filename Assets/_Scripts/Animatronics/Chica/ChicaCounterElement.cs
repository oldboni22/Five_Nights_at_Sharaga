using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChicaCounterElement : MonoBehaviour
{
    [SerializeField] private Image _image;
    public void SetColor(Color color)
    {
        _image.color = color;
    }
}
