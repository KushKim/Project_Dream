using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerdead : MonoBehaviour
{
    public GameObject hpbar;
    public GameObject youdie;
    void Update()
    {
        
        if (hpbar.transform.gameObject.GetComponent<Image>().fillAmount == 0)
        {
            Time.timeScale = 0.5f;
            youdie.SetActive(true);
        }

    
    }
}
