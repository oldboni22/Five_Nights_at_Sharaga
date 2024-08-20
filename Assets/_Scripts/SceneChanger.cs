using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Button))]
public class SceneChanger : MonoBehaviour
{
    [SerializeField] private AssetReference _sceneRef;

    void OpenScene()
    {
        SceneOpener.OpenScene(_sceneRef);
        GetComponent<Button>().interactable = false;
    }
    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(OpenScene);
    }
}
