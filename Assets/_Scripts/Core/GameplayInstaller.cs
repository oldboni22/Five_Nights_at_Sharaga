using UnityEngine;
using Zenject;

public class GameplayInstaller : MonoInstaller
{
    [SerializeField] private VentButton _vent;
    [SerializeField] private RoomsController _roomsController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private CameraButtonsController _cameraButtonsController;
    [SerializeField] private AudioListenerController _audioListenerController;
    [SerializeField] private PatienceBar _patienceBar;
    [SerializeField] private SceneController _sceneController;
    [SerializeField] private OpenCameraButton _openCameraButton;

    [SerializeField] GameObject _overlayPrebab;

    

    public override void InstallBindings()
    {

        Container.Bind<ISceneController>().To<SceneController>().FromInstance(_sceneController).AsSingle().NonLazy();
        Container.Bind<IVentButton>().To<VentButton>().FromInstance(_vent).AsSingle().Lazy();


        Container.Bind<IOpenCameraButton>().To<OpenCameraButton>().FromInstance(_openCameraButton).AsCached().NonLazy();

        Container.Bind<IActionPointsManager>().To<ActionPointsManager>().AsSingle().NonLazy();

        Container.Bind<INightTimer>().To<NightTimer>().AsSingle().NonLazy();
        Container.Bind<IClock>().To<Clock>().AsSingle().NonLazy();


        Container.Bind<IPlayer>().To<Player>().FromNew().AsSingle().NonLazy();
        Container.Bind<IServiceManager>().To<ServiceManager>().FromNew().AsSingle().NonLazy();

        

        Container.Bind<ICameraButtonsController>().To<CameraButtonsController>().FromInstance(_cameraButtonsController).AsSingle().NonLazy();
        Container.Bind<IRoomsController>().To<RoomsController>().FromInstance(_roomsController).AsSingle().NonLazy();
        Container.Bind<ICameraControler>().To<CameraController>().FromInstance(_cameraController).AsSingle().NonLazy();
        Container.Bind<IPatienceBar>().To<PatienceBar>().FromInstance(_patienceBar).AsSingle().Lazy();


        Container.Bind<IAudioListenerController>().FromInstance(_audioListenerController).AsSingle().Lazy();
        Container.BindMemoryPool<OverlayImage, OverlayImage.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_overlayPrebab).UnderTransformGroup("Camera Overlays");
        
    }
}