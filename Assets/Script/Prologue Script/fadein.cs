using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadein : MonoBehaviour
{
    public GameObject 제거대상;
    public int DelayTime;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", DelayTime);
    }

    // Update is called once per frame
    void DestroyObject()
    {
        Destroy(제거대상);
    }
}
