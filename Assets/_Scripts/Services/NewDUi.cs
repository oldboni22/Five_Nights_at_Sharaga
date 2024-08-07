using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class NewDUi : MonoBehaviour
{

    private NewDCharge _newd;

    [SerializeField] private HoldableButton _button;
    [SerializeField] private Slider _slider;

    [SerializeField] private float _charge;

    [Inject]
    public void Inject(IServiceManager service)
    {
        _newd = service.GetCharge();
    }

    void OnChargeChanged(float val)
    {
        _slider.value = val;
    }

    private void Start()
    {
        _button.AddOnHoldListener(() => { _newd.Charge(_charge); });

        _newd.AddOnChargeChangedListener(OnChargeChanged);
    }

}
