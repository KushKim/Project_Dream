﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Image image;
    public string sceneName;
    private float time;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        image.gameObject.SetActive(true);
        time += Time.deltaTime / 1.5f;
        Color color = image.color;
        color.a = Mathf.Lerp(0, 1, time);
        image.color = color;
        if (image.color.a >= 0.9999f)
        {
            Player.playerHP = 3;
            SceneManager.LoadScene(sceneName);
        }
    }
}
