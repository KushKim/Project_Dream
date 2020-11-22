using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class touchtostart : MonoBehaviour
{
    public void OnClickChange()
    {
        LoadingSceneManager.LoadScene("loadscene");
    }
    public void OnClickSkip()
    {
        LoadingSceneManager.LoadScene("loadscene");
    }


}
