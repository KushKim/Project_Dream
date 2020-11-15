using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fadein : MonoBehaviour
{
    public GameObject 제거대상;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestroyObject", 3f);
    }

    // Update is called once per frame
    void DestroyObject()
    {
        Destroy(제거대상);
    }
}
