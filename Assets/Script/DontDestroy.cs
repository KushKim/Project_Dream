using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().name != "Level1"|| SceneManager.GetActiveScene().name != "Level2"|| SceneManager.GetActiveScene().name != "Level3")
        {
            Destroy(this.gameObject);
        }
    }
}
