using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StoragesInstaller", menuName = "Installers/StoragesInstaller")]
public class StoragesInstaller : ScriptableObjectInstaller<StoragesInstaller>
{
    [SerializeField] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private CameraImagesStorage _cameraImagesStorage;
    [SerializeField] private SoundStorage _soundStorage;
    public override void InstallBindings()
    {
        Container.Bind<AnimatronicsDifficulty>().FromInstance(_animatronicsDifficulty).AsSingle().NonLazy();
        Container.Bind<CameraImagesStorage>().FromInstance(_cameraImagesStorage).AsSingle().NonLazy();
        Container.Bind<SoundStorage>().FromInstance(_soundStorage).AsSingle().NonLazy();

    }
}