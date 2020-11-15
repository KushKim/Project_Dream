using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startbtinvoke : MonoBehaviour
{
    public GameObject StartBT;
    public int delaytime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("btdelay", delaytime);
    }

    // Update is called once per frame
    void btdelay()
    {
        StartBT.gameObject.SetActive(true);
    }
}
