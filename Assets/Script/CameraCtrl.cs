using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    Transform tr;
    public float xPos = 8f;
    // Start is called before the first frame update
    void Start()
    {
        tr = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(tr.position.x + xPos, 0f, -10f);
    }
}
