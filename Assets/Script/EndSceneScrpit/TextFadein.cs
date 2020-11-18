using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFadein : MonoBehaviour
{
    public GameObject Text;
    public int ShowDelay;

    private void Start()
    {
        Invoke("TextDelay", ShowDelay);
    }

    void TextDelay()
    {
        Text.gameObject.SetActive(true);
    }
}
