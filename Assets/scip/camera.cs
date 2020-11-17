using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public GameObject player;
    Transform AT;
    public float a = 8f;
    
    // Start is called before the first frame update
    void Start()
    {
        AT = player.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(AT.position.x + a, 0f, -10f);
    }
}
