
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public static class SceneOpener
{
    public static void OpenMainScene()
    {
        SceneManager.LoadScene(0,LoadSceneMode.Single);
    }
    public static void OpenScene(AssetReference sceneRef)
    {
        Addressables.LoadSceneAsync(sceneRef, LoadSceneMode.Single);
    }
}
