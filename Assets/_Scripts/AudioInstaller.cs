using System.Collections;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;


public class AudioInstaller : MonoInstaller
{
    [SerializeField] GameObject _audioPrefab;
    [SerializeField] GameObject _3DAudioPrefab;
    
    [SerializeField] ScreamerUiController _screamerUiController;

    public override void InstallBindings()
    {
        Container.BindMemoryPool<AudioPlayer, AudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_audioPrefab).UnderTransformGroup("Audio").AsCached().NonLazy();
        Container.BindMemoryPool<SurrondAudioPlayer, SurrondAudioPlayer.Pool>().WithInitialSize(3).FromComponentInNewPrefab(_3DAudioPrefab).UnderTransformGroup("3DAudio").AsCached().NonLazy();
        
        Container.Inject(_screamerUiController);
        Container.Bind<IScreamerUiController>().To<ScreamerUiController>().FromInstance(_screamerUiController).AsCached().NonLazy();
    }

}