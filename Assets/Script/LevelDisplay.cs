using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelDisplay : MonoBehaviour
{
    public GameObject leveltext;
    public int fadeoutTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("fadeOut", fadeoutTime);
    }

    // Update is called once per frame
    void fadeOut()
    {
        leveltext.gameObject.SetActive(false);
    }
}
