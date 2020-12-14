using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    static private DontDestroy instance;

    private void Awake()
    {
        if(instance != null)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Level1"&& SceneManager.GetActiveScene().name != "Level2"&& SceneManager.GetActiveScene().name != "Level3")
        {
            Destroy(this.gameObject);
        }
    }
}
