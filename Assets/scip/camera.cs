using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject A;
    Transform AT;
    
    // Start is called before the first frame update
    void Start()
    {
        AT = A.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(AT.position.x + 8f, 0f, -10f);
    }
}
