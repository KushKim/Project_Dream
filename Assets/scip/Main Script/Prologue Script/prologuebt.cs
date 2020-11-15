using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class prologuebt : MonoBehaviour
{
    public GameObject ClickToStart;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("btdelay", 14f);
    }

    // Update is called once per frame
    void btdelay()
    {
        Debug.Log("14초 경과");
        ClickToStart.gameObject.SetActive(true);
    }
}
