using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartDirector : MonoBehaviour
{
     public void OnclickStart()
    {
        SceneManager.LoadScene("Prologue");
    }
}
