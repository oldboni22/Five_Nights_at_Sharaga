using UnityEngine;
using Zenject;

[CreateAssetMenu(fileName = "StoragesInstaller", menuName = "Installers/StoragesInstaller")]
public class StoragesInstaller : ScriptableObjectInstaller<StoragesInstaller>
{
    [SerializeField] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private CameraImagesStorage _cameraImagesStorage;
    [SerializeField] private SoundStorage _soundStorage;
    [SerializeField] private AmbienceStorage _ambienceStorage;
    [SerializeField] private PipkiPopkiStorage _pipkiPopki;
    [SerializeField] private NewdStorage _newD;
    [SerializeField] private RandomSoundStorage _randomSounds;
    [SerializeField] private AnimatronicSpriteStorage _animatronics;
    public override void InstallBindings()
    {
        Container.Bind<AnimatronicsDifficulty>().FromInstance(_animatronicsDifficulty).AsCached().NonLazy();
        Container.Bind<CameraImagesStorage>().FromInstance(_cameraImagesStorage).AsCached().NonLazy();
        Container.Bind<SoundStorage>().FromInstance(_soundStorage).AsCached().NonLazy();
        Container.Bind<AmbienceStorage>().FromInstance(_ambienceStorage).AsCached().NonLazy();
        Container.Bind<PipkiPopkiStorage>().FromInstance(_pipkiPopki).AsCached().NonLazy();
        Container.Bind<NewdStorage>().FromInstance(_newD).AsCached().NonLazy();
        Container.Bind<RandomSoundStorage>().FromInstance(_randomSounds).AsCached().NonLazy();
        Container.Bind<AnimatronicSpriteStorage>().FromInstance(_animatronics).AsCached().NonLazy();

    }
}