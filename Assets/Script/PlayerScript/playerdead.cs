using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerdead : MonoBehaviour
{
    public GameObject youdie;
    public Image image;
    public AudioSource audioSource;

    private void Start()
    {
        youdie = GameObject.Find("Canvas").transform.Find("Youdie").gameObject;
        image = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        youdie = GameObject.Find("Canvas").transform.Find("Youdie").gameObject;
        image = GameObject.Find("Canvas").transform.Find("Fade").GetComponent<Image>();
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

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
        audioSource.volume -= 0.4f * Time.deltaTime;
        yield return new WaitForSeconds(3.5f);
        Player.playerHP = 3;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainStart");
    }
}
