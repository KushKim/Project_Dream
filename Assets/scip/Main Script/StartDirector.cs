using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
    private Animator animator;
    private GameObject Fadein;
    /*private void Awake()
    {
        Fadein = GameObject.Find("Canvas").transform.Find("fadein").gameObject;
    }
    */
    public void OnclickStart()
    {
        //Fadein.SetActive(true);
        //animator.SetTrigger("fadein");
        Invoke("InvokeAni", 4f);
    }
    void InvokeAni()
    {
        SceneManager.LoadScene("Prologue");
    }
}
