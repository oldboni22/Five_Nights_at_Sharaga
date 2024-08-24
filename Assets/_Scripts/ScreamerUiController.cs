using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Zenject;


public interface IScreamerUiController
{
    void Jumpscare(string id, bool displayDeathImg);
}
public class ScreamerUiController : MonoBehaviour, IScreamerUiController
{
    [SerializeField] private float _dur;
    [SerializeField] private AssetReferenceGameObject _uiRef;
    private GameObject _ui;

    [SerializeField] private Sprite _gameOverSprite;

    [Inject] private AudioPlayer.Pool _audio;
    [Inject] private SurrondAudioPlayer.Pool _audio3D;
    [Inject] private AnimatronicSpriteStorage _spriteStorage;

    private void Awake()
    {
        _uiRef.LoadAssetAsync<GameObject>().Completed += (x) =>
        {
            _ui = x.Result;
            Debug.Log("Start");
            Debug.Log(_ui);
        };
    }

    public void Jumpscare(string id, bool displayDeathImg)
    {
        _audio.StopAudio(null);
        _audio3D.StopAudio(null);

        StartCoroutine(Screamer(id,displayDeathImg));
    }

    private IEnumerator Screamer(string id,bool displayDeathImg)
    {
        var ui = GameObject.Instantiate(_ui).GetComponent<ScreamerUi>();

        ui.SetSprite(_spriteStorage.GetMemberById(id).Sprite);
        _audio.PlayAudio("jumpscare", 1, 255);

        yield return new WaitForSeconds(_dur);
        if (displayDeathImg)
        {
            ui.SetSprite(_gameOverSprite);
            yield return new WaitForSeconds(_dur);
        }
        SceneOpener.OpenMainScene();
    }


}
