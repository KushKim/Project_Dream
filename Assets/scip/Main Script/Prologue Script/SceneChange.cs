using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class SceneChange : MonoBehaviour
{
    public float DelayTime;
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

            Debug.Log("활성화완료");
        }
      
    }
    
    void ActiveTrue()
    {
        GameObject.Find("Canvas").transform.Find("ClickToStart").gameObject.SetActive(true);
    }

}
