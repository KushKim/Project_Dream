using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneChange : MonoBehaviour
{
    public float DelayTime;
    public GameObject StartBT;
private void Start()
    {


        StartCoroutine(Active());
        Invoke("ActiveTrue", DelayTime);
    }

    
    IEnumerator Active()
    {
        while(true)
        {
            yield return null;

            
        }
      
    }
    

private void ActiveTrue()
    {
        StartBT.gameObject.SetActive(true);
    }
    
}

