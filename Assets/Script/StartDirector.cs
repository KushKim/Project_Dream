using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    public GameObject fadein;


    public void OnclickStart()
    {
        fadein.gameObject.SetActive(true);
        Invoke("InvokeAni", 4f);
    }
    void InvokeAni()
    {
        SceneManager.LoadScene("prologue");
    }
}
