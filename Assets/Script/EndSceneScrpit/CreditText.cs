using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditText : MonoBehaviour
{
    public GameObject credit;
    public int showtime;

    private void Start()
    {
        Invoke("showdelay", showtime);
    }

    void showdelay ()
    {
        credit.gameObject.SetActive(true);
    }
}
