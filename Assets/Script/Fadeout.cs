using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public Image fade;
    private bool fadeoff;

    void Update()
    {
        if (!fadeoff)
        {
            fade.color = new Color(0, 0, 0, Mathf.Lerp(fade.color.a, 0, 3 * Time.deltaTime));
            if (fade.color.a <= 0.0001f)
            {
                fade.gameObject.SetActive(false);
                fadeoff = true;
            }
        }
    }
}
