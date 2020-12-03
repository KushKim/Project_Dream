using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerdead : MonoBehaviour
{
    public GameObject youdie;
    void Update()
    {
        
        if (Player.playerHP == 0) // 클래스에 있는 HP 값으로 수정
        {
            Time.timeScale = 0.5f;
            youdie.SetActive(true);
        }

    
    }
}
