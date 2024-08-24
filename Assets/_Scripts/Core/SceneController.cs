using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

public interface ISceneController
{
    public void StopUpdate();
}

public class SceneController : MonoBehaviour, ISceneController
{
    private bool _doUpdate = true;

    [Inject] private IServiceManager _service;
    [Inject] private IClock _clock;
    [Inject] private IActionPointsManager _actionPointsManager;
    [Inject] private IPlayer _player;


    [SerializeField] private IUpdateable[] _updateables;
    [SerializeField] private IFixedUpdateable[] _fixedUpdateables;
    [SerializeField] private IClockable[] _clockables;

    [SerializeField] private float _clockRate;

    private void Awake()
    {
        Debug.Log("Awake");

        _player.OnAwake();
        foreach (var awakeable in GetComponentsInChildren<IAwakable>())
        {
            awakeable.OnAwake();
        }
    }

    private void Start()
    {
        Debug.Log("Start");

        _updateables = GetComponentsInChildren<IUpdateable>();
        _fixedUpdateables = GetComponentsInChildren<IFixedUpdateable>();
        _clockables = GetComponentsInChildren<IClockable>();

        _clock.SetTimeToTick(_clockRate);
        foreach (var clockable in _clockables)
            _clock.Add(clockable);
    }

    private void Update()
    {
        if (_doUpdate is false)
            return;

        _service.OnUpdate();
        _clock.OnUpdate();
        foreach(var updateable in _updateables)
            updateable.OnUpdate();
    }

    private void FixedUpdate()
    {
        if(_doUpdate is false)
            return;

        _actionPointsManager.OnFixedUpdate();
        foreach (var updateable in _fixedUpdateables)
            updateable.OnFixedUpdate();
    }


    public void StopUpdate() => _doUpdate = false;
}
