using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private RoomsController _roomsController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private CameraButtonsController _cameraButtonsController;

    [SerializeField] GameObject _audioPrefab;
    [SerializeField] GameObject _3DAudioPrefab;
    [SerializeField] private AudioListenerController _audioListenerController;

    public override void InstallBindings()
    {
        Container.Bind<IPlayer>().To<Player>().AsSingle().NonLazy();

        Container.Bind<IActionPointsManager>().To<ActionPointsManager>().AsSingle().NonLazy(); 
        Container.Bind<IClock>().To<Clock>().AsSingle().NonLazy();

        Container.Bind<ICameraButtonsController>().To<CameraButtonsController>().FromInstance(_cameraButtonsController).AsSingle().NonLazy();
        Container.Bind<IRoomsController>().To<RoomsController>().FromInstance(_roomsController).AsSingle().NonLazy();
        Container.Bind<ICameraControler>().To<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();

        Container.Bind<IAudioListenerController>().FromInstance(_audioListenerController).AsSingle().NonLazy();

        Container.BindMemoryPool<AudioPlayer,AudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_audioPrefab).UnderTransformGroup("Audio").NonLazy();
        Container.BindMemoryPool<SurrondAudioPlayer, SurrondAudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_3DAudioPrefab).UnderTransformGroup("3DAudio").NonLazy();
        
    }
}