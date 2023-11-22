using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class LoadingScene : MonoBehaviour
{
    private AsyncOperation _loadingSceneOperation;
    [SerializeField] private Image fillLoadBar;
    [SerializeField] private Button playButton;

    private void Start()
    {
        fillLoadBar.fillAmount = 0f;
        playButton.onClick.AddListener(LoadMainScene);
        // StartCoroutine(SmoothlyChangeVariable());
    }

    private void Update()
    {
        Tes();
    }

    private void Tes()
    {
        /* if (fillLoadBar.fillAmount < 1)
        {
            fillLoadBar.fillAmount += 0.002f;
        }
        else
        {
            Debug.Log("in else");
            if (!playButton.isActiveAndEnabled)
            {
                Debug.Log("in if");
                playButton.gameObject.SetActive(true);
            }
        } */
    }    
    

    private void LoadMainScene()
    {
        StartCoroutine(UnloadScene());
        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        yield return _loadingSceneOperation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(1));
    }

    private IEnumerator UnloadScene()
    {
        var sceneToUnload = SceneManager.GetSceneByBuildIndex(0);
        if (sceneToUnload.IsValid())
        {
            yield return SceneManager.UnloadSceneAsync(sceneToUnload);
        }
    }
}
