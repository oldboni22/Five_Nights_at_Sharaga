using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;

public interface ISceneController
{
    public void StopUpdate();
    public event Action OnStoped;
}

public class SceneController : MonoBehaviour, ISceneController
{

    public event Action OnStoped;

    private bool _doUpdate = true;
    [SerializeField] private NightUi _ui;

    [SerializeField] private AssetReference _minigameScene;
    [Inject] private INightTimer _timer;
    [Inject] private IServiceManager _service;
    [Inject] private IClock _clock;
    [Inject] private IActionPointsManager _actionPointsManager;
    [Inject] private IPlayer _player;


    private IUpdateable[] _updateables;
    private IFixedUpdateable[] _fixedUpdateables;
    private IClockable[] _clockables;

    [SerializeField] private float _clockRate;

    private async void Awake()
    {

        _timer.OnHourPass += async (t) => await EndNight(t);
        _doUpdate = false;
        Debug.Log("Awake");

        await _ui.PlayStartAnimation();


        _player.OnAwake();
        foreach (var awakeable in GetComponentsInChildren<IAwakable>())
        {
            awakeable.OnAwake();
        }
        _doUpdate = true;
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
    async Task EndNight(ushort t)
    {
        if (t < 6) return;

        StopUpdate();
        await _ui.PlayEndAnimation();
        SceneOpener.OpenScene(_minigameScene);
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

        _timer.OnFixedUpdate();
        _actionPointsManager.OnFixedUpdate();
        foreach (var updateable in _fixedUpdateables)
            updateable.OnFixedUpdate();
    }


    public void StopUpdate()
    {
        _doUpdate = false;
        OnStoped?.Invoke();
    }

}
