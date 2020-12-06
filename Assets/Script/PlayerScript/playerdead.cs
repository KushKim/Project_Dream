﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerdead : MonoBehaviour
{
    public GameObject youdie;
    public Image image;
    public AudioSource audioSource;
    void Update()
    {
        
        if (Player.playerHP == 0) // 클래스에 있는 HP 값으로 수정
        {
            Time.timeScale = 0.5f;
            StartCoroutine(FadeOut());
        }
    }

    IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(2f);
        youdie.SetActive(true);
        image.gameObject.SetActive(true);
        image.color = new Color(0, 0, 0, image.color.a + 1 * Time.deltaTime);
        audioSource.volume -= 0.3f * Time.deltaTime;
        yield return new WaitForSeconds(3.5f);
        Player.playerHP = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainStart");
    }
}
