using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Image image;
    public string sceneName;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        image.gameObject.SetActive(true);
        image.color = new Color(0, 0, 0, Mathf.Lerp(image.color.a, 1, 3 * Time.deltaTime));
        if (image.color.a >= 0.9999f)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
