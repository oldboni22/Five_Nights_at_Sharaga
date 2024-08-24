using System.ComponentModel;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Zenject;

public class GloballInstaller : MonoInstaller
{

    [SerializeField] GameObject _audioPrefab;
    [SerializeField] GameObject _3DAudioPrefab;

    [SerializeField] ScreamerUiController _screamerUiController;

    [SerializeField] private AssetReferenceStorages _ref;

    public override void InstallBindings()
    {

        _ref.LoadAssetAsync<StoragesRef>().Completed += Bind;
     
    }

    void Bind(AsyncOperationHandle<StoragesRef> @ref)
    {
        Container.Bind<AnimatronicsDifficulty>().FromInstance(@ref.Result.AnimatronicsDifficulty).AsCached().NonLazy();
        Container.Bind<CameraImagesStorage>().FromInstance(@ref.Result.CameraImagesStorage).AsCached().NonLazy();
        Container.Bind<SoundStorage>().FromInstance(@ref.Result.SoundStorage).AsCached().NonLazy();
        Container.Bind<AmbienceStorage>().FromInstance(@ref.Result.AmbienceStorage).AsCached().NonLazy();
        Container.Bind<PipkiPopkiStorage>().FromInstance(@ref.Result.PipkiPopki).AsCached().NonLazy();
        Container.Bind<NewdStorage>().FromInstance(@ref.Result.NewD).AsCached().NonLazy();
        Container.Bind<RandomSoundStorage>().FromInstance(@ref.Result.RandomSounds).AsCached().NonLazy();
        Container.Bind<AnimatronicSpriteStorage>().FromInstance(@ref.Result.Animatronics).AsCached().NonLazy();
        Container.Bind<JumperLayoutStorage>().FromInstance(@ref.Result.JumperLayoutStorage).AsCached().NonLazy();



        Container.BindMemoryPool<AudioPlayer, AudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_audioPrefab).UnderTransformGroup("Audio").AsCached().NonLazy();
        Container.BindMemoryPool<SurrondAudioPlayer, SurrondAudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_3DAudioPrefab).UnderTransformGroup("3DAudio").AsCached().NonLazy();

        Container.Inject(_screamerUiController);
        Container.Bind<IScreamerUiController>().To<ScreamerUiController>().FromInstance(_screamerUiController).AsCached().NonLazy();
    }
    
}