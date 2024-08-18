using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public interface IPatienceBar
{
    public void OnPatienceChanged(float val);
    public void SetText(string floor);
    public void SetText();
}
public class PatienceBar : MonoBehaviour, IPatienceBar
{
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Slider _slider;

    public void OnPatienceChanged(float val)
    {
        _slider.value = val;
    }
    public void SetText(string floor)
    {
        _text.text = floor + " floor";
    }
    public void SetText()
    {
        _text.text = " ";
    }
}
