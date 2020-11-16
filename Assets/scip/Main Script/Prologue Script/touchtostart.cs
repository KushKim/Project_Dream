using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class touchtostart : MonoBehaviour
{
    public void OnClickChange()
    {
        SceneManager.LoadScene("Level1");
    }
    public void OnClickSkip()
    {
        SceneManager.LoadScene("Level1");
    }


}
