using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class njump2 : MonoBehaviour
{
    public float movespeed = 5f;
    public float jumpspeed = 5f;
    public bool isGrounded = false;
    public int jumpcount = 2;
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpcount = 0;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
            if (collision.gameObject.tag == "Ground")
            {
                isGrounded = true;
                jumpcount = 2;
            }
    }

    void Update()
    {
            if (jumpcount > 0)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    rb.AddForce(new Vector2(0, 1) * jumpspeed, ForceMode2D.Impulse);
                    jumpcount--;
                }
            }
    }
}