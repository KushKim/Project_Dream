using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private bool pauseOn = false;
    private GameObject normalPanel;
    private GameObject pausePanel;
    private GameObject jumpBT;
    private GameObject slideBT;
    private AudioManager audioManager;

    private void Awake()
    {
        normalPanel = GameObject.Find("Canvas").transform.Find("NormalUI").gameObject;
        pausePanel = GameObject.Find("Canvas").transform.Find("PauseUI").gameObject;
        jumpBT = GameObject.Find("Canvas").transform.Find("JumpButton").gameObject;
        slideBT = GameObject.Find("Canvas").transform.Find("SlideButton").gameObject;
        audioManager = FindObjectOfType<AudioManager>();

    }
    public void ActivePauseBt()
    {
        if (!pauseOn)
        {
            audioManager.Play("메뉴클릭");
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            normalPanel.SetActive(false);
            jumpBT.SetActive(false);
            slideBT.SetActive(false);
        }
        else
        {
            audioManager.Play("메뉴클릭");
            Time.timeScale = 1.0f;
            pausePanel.SetActive(false);
            normalPanel.SetActive(true);
            jumpBT.SetActive(true);
            slideBT.SetActive(true);
        }
        pauseOn = !pauseOn;
    }

    public void RetryBt()
    {
        audioManager.Play("메뉴클릭");
        Debug.Log("Restart Game");
        Time.timeScale = 1.0f;
        Player.playerHP = 3;
        SceneManager.LoadScene("Level1");
    }
    public void QuitBt()
    {
        audioManager.Play("메뉴클릭");
        Debug.Log("Quit Game");
        Time.timeScale = 1.0f;
        Player.playerHP = 3;
        SceneManager.LoadScene("MainStart");
    }
    public void LV2RetryBt()
    {
        audioManager.Play("메뉴클릭");
        Time.timeScale = 1.0f;
        Player.playerHP = 3;
        SceneManager.LoadScene("Level2");
    }
    public void LV3RetryBt()
    {
        audioManager.Play("메뉴클릭");
        Time.timeScale = 1.0f;
        Player.playerHP = 3;
        SceneManager.LoadScene("Level3");
    }
}

