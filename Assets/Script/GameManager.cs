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

    private void Awake()
    {
        normalPanel = GameObject.Find("Canvas").transform.Find("NormalUI").gameObject;
        pausePanel = GameObject.Find("Canvas").transform.Find("PauseUI").gameObject;
        jumpBT = GameObject.Find("Canvas").transform.Find("JumpButton").gameObject;
        slideBT = GameObject.Find("Canvas").transform.Find("SlideButton").gameObject;
    }
    public void ActivePauseBt()
    {
        if (!pauseOn)
        {
            
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            normalPanel.SetActive(false);
            jumpBT.SetActive(false);
            slideBT.SetActive(false);
        }
        else
        {
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
        Debug.Log("Restart Game");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("Level1");
    }
    public void QuitBt()
    {
        Debug.Log("Quit Game");
        Time.timeScale = 1.0f;
        SceneManager.LoadScene("MainStart");
    }
}

