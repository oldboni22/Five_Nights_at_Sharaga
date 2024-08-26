using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;
    [Inject]
    public void Inject(INightTimer timer)
    {
        timer.OnHourPass += (time) => { _text.text = $"{time}AM"; };
    }
}
