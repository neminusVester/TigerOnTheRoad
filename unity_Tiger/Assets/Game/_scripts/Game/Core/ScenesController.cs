using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesController : MonoBehaviour
{
    [SerializeField] private Image fillLoadBar;
    [SerializeField] private Canvas canvas;
    private AsyncOperation _loadingSceneOperation;
    private AsyncOperation _unloadingSceneOperation;

    private void Start()
    {
        GameEvents.Instance.OnPlayPressed += LoadGameScene;
        GameEvents.Instance.OnHomePressed += LoadHomeScene;
        GameEvents.Instance.OnRestartPressed += ReloadGameScene;
        StartCoroutine(LoadingAnimation());
    }

    private void OnDestroy()
    {
        GameEvents.Instance.OnPlayPressed -= LoadGameScene;
        GameEvents.Instance.OnHomePressed -= LoadHomeScene;
        GameEvents.Instance.OnRestartPressed -= ReloadGameScene;
    }


    private void LoadHomeScene()
    {
        StartCoroutine(UnloadScene(ScenesInBuild.GameScene));
        StartCoroutine(LoadScene(ScenesInBuild.HomeScene));
        SoundController.Instance.PlayMainBackgroundMusic();
    }

    private void LoadGameScene()
    {
        StartCoroutine(UnloadScene(ScenesInBuild.HomeScene));
        SoundController.Instance.StopBackgroundMusic();
        StartCoroutine(LoadScene(ScenesInBuild.GameScene));
    }

    private void ReloadGameScene()
    {
        StartCoroutine(UnloadScene(ScenesInBuild.GameScene));
        StartCoroutine(LoadScene(ScenesInBuild.GameScene));
    }

    private IEnumerator LoadScene(ScenesInBuild scene)
    {
        var sceneIndex = (int)scene;
        yield return _loadingSceneOperation = SceneManager.LoadSceneAsync(sceneIndex, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneIndex));
    }

    private IEnumerator UnloadScene(ScenesInBuild scene)
    {
        var sceneIndex = (int)scene;
        var sceneToUnload = SceneManager.GetSceneByBuildIndex(sceneIndex);
        if (sceneToUnload.IsValid())
        {
            yield return _unloadingSceneOperation = SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }

    private IEnumerator LoadingAnimation()
    {
        float elapsedTime = 0f;
        float startValue = 0f;
        float endValue = 1f;

        while (elapsedTime < 3f)
        {
            fillLoadBar.fillAmount = Mathf.Lerp(startValue, endValue, elapsedTime / 3f);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        LoadHomeScene();
        Destroy(canvas.gameObject);
    }

    public enum ScenesInBuild
    {
        MainScene,
        HomeScene,
        GameScene
    }
}
