using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public class SceneController : MonoBehaviour 
{
    [Inject] private IClock _clock;
    [Inject] private IActionPointsManager _actionPointsManager;

    [SerializeField] private IAwakable[] _awakeables;
    [SerializeField] private IUpdateable[] _updateables;
    [SerializeField] private IFixedUpdateable[] _fixedUpdateables;
    [SerializeField] private IClockable[] _clockables;

    [SerializeField] private float _clockRate;

    private void Awake()
    {
        _awakeables = GetComponentsInChildren<IAwakable>();
        _updateables = GetComponentsInChildren<IUpdateable>();
        _fixedUpdateables = GetComponentsInChildren<IFixedUpdateable>();
        _clockables = GetComponentsInChildren<IClockable>();

        foreach (var awakeable in _awakeables)
        {
            awakeable.OnAwake();
        }
    }

    private void Start()
    {
        foreach (var clockable in _clockables)
            _clock.Add(clockable);
        
        StartCoroutine(Tick());
    }

    private void Update()
    {
        foreach(var updateable in _updateables)
            updateable.OnUpdate();
    }

    private void FixedUpdate()
    {
        _actionPointsManager.OnFixedUpdate();
        foreach (var updateable in _fixedUpdateables)
            updateable.OnFixedUpdate();
    }

    IEnumerator Tick()
    {
        yield return new WaitForSeconds(_clockRate);
        _clock.Tick();
        StartCoroutine(Tick());
    }
}
