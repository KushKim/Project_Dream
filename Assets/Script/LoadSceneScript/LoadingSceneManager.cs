using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//Top of the script
#pragma warning disable 0649
public class LoadingSceneManager : MonoBehaviour
{

    [SerializeField]
    private Image loadingBar;

    private void Start()
    {
        loadingBar.fillAmount = 0;
        StartCoroutine(LoadAsyncScene());
    }
    public static void LoadScene(string sceneName)
    {
        SceneManager.LoadScene("loadscene");
    }
    IEnumerator LoadAsyncScene()
    {
        yield return null;
        AsyncOperation asyncScene = SceneManager.LoadSceneAsync("Level1");
        asyncScene.allowSceneActivation = false;
        float timeC = 0;
        while (!asyncScene.isDone)
        {
            yield return null;
            timeC += Time.deltaTime;
            if (asyncScene.progress >= 0.9f)
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, 1, timeC);
                if (loadingBar.fillAmount == 1.0f)
                {
                    asyncScene.allowSceneActivation = true;
                }
            }
            else
            {
                loadingBar.fillAmount = Mathf.Lerp(loadingBar.fillAmount, asyncScene.progress, timeC);
                if (loadingBar.fillAmount >= asyncScene.progress)
                {
                    timeC = 0f;
                }
            }
        }
    }
}