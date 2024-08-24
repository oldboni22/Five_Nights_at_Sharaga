
using UnityEngine;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName ="Storages")]
public class StoragesRef : ScriptableObject
{
    [SerializeField] private AnimatronicsDifficulty _animatronicsDifficulty;
    [SerializeField] private CameraImagesStorage _cameraImagesStorage;
    [SerializeField] private SoundStorage _soundStorage;
    [SerializeField] private AmbienceStorage _ambienceStorage;
    [SerializeField] private PipkiPopkiStorage _pipkiPopki;
    [SerializeField] private NewdStorage _newD;
    [SerializeField] private RandomSoundStorage _randomSounds;
    [SerializeField] private AnimatronicSpriteStorage _animatronics;
    [SerializeField] private JumperLayoutStorage _jumperLayoutStorage;

    public AnimatronicsDifficulty AnimatronicsDifficulty { get => _animatronicsDifficulty; }
    public CameraImagesStorage CameraImagesStorage { get => _cameraImagesStorage; }
    public SoundStorage SoundStorage { get => _soundStorage;}
    public AmbienceStorage AmbienceStorage { get => _ambienceStorage; }
    public PipkiPopkiStorage PipkiPopki { get => _pipkiPopki; }
    public NewdStorage NewD { get => _newD; }
    public RandomSoundStorage RandomSounds { get => _randomSounds; }
    public AnimatronicSpriteStorage Animatronics { get => _animatronics; }
    public JumperLayoutStorage JumperLayoutStorage { get => _jumperLayoutStorage;}
}

[System.Serializable]
public class AssetReferenceStorages : AssetReferenceT<StoragesRef>
{
    public AssetReferenceStorages(string guid) : base(guid)
    {
    }
}