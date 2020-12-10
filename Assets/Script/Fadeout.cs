using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fadeout : MonoBehaviour
{
    public Image fade;
    private bool fadeoff;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
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
