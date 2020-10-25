using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movingclass : MonoBehaviour
{
    SpriteRenderer spd;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        spd = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Move2();
    }
    void Move()
    {
        Vector3 movemont = Vector3.zero;
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            movemont = Vector3.right;
            spd.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            movemont = Vector3.left;
            spd.flipX = true;
        }
        transform.position += movemont * Time.deltaTime * speed;
    }
    void Move2()
    {
        Vector3 movemont = Vector3.zero;
        if (Input.GetAxisRaw("Vertical") > 0)
        {
            movemont = Vector3.up;
        }
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            movemont = Vector3.down;
        }
        transform.position += movemont * Time.deltaTime * speed;
    }
}
