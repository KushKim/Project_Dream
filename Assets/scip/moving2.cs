using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving2 : MonoBehaviour
{
    public float h;
    public Transform tr;
    private Animator anim;
    private Rigidbody2D rig;
    private bool isGround;
    public float jumppower = 10;
    private SpriteRenderer sp;
    int doujump;

    void Start()
    {
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
        Ani();
        jump();
    }

    void Ani()
    {
        if (h == 0)
        {
            anim.SetBool("Run", false);
        }
        else
        {
            anim.SetBool("Run", true);
        }
        if (isGround == false)
        {
            anim.SetBool("Jump", true);
        }
        else
        {
            anim.SetBool("Jump", false);
        }
    }
    //땅에 있는지의 여부
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGround = true;
    }
    //점프
    void jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isGround == true)
            {
                rig.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
                isGround = false;
            }
            else
            {
                return;
            }
        }
    }
}
