using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFadein : MonoBehaviour
{
    public GameObject Text;
    public int ShowDelay;
    public int DestroyDelay;

    private void Start()
    {
        Invoke("TextDelay", ShowDelay);
        Invoke("Destroy", DestroyDelay);
    }

    void TextDelay()
    {
        Text.gameObject.SetActive(true);
    }

    void Destroy()
    {
        Destroy(Text);
    }
}
