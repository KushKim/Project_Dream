using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float moveSpeed = 8.5f;
    public float jumpPower = 6f;
    private Transform Pos;
    private Rigidbody2D rigd;
    public int jumpCount = 2;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
       Pos = GetComponent<Transform>();
       rigd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Pos.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpCount > 0)
            {
                anim.SetInteger("Jump", jumpCount);
                anim.SetBool("JumpOn", true);
                rigd.velocity=new Vector2(0,jumpPower);
                jumpCount--;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            jumpCount = 2;
            anim.SetBool("JumpOn", false);
        }
    }
}
