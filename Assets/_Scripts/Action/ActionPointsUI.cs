using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ActionPointsUI : MonoBehaviour, IAwakable
{
    [Inject] private IActionPointsManager _manager;
    [SerializeField] private Slider _slider;
    [SerializeField] private TMP_Text _text;

    public void OnAwake()
    {
        _manager.AddEventListener(UpdateUi);
    }


    void UpdateUi(float charge, int points)
    {
        _slider.value = charge;
        _text.text = $"{points}";
    }
}
