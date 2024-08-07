using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActionPointsUI : MonoBehaviour
{
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    [Inject]
    public void Inject(IActionPointsManager manager)
    {
        manager.AddOnChargeEventListener(UpdateCharge);
        manager.AddOnPointsEventListener(UpdatePoints);
    }

    void UpdatePoints(int points)
    {
        _text.text = $"{points}";
    }

    void UpdateCharge(float charge)
    {
        _slider.value = charge;
    }
}
