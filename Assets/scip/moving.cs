using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moving : MonoBehaviour
{
    public float movespeed = 5.0f;
    public Transform tr;
    public float h;
    private Animator anim;
    private Rigidbody2D rig;
    private bool isGround;
    public float jumppower = 10;
    private SpriteRenderer sp;
    // Start is called before the first frame update
    void Start()
    {
        tr = GetComponent<Transform>();
        anim = GetComponent<Animator>();
        rig = GetComponent<Rigidbody2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");

        tr.Translate(Vector2.right * movespeed * h * Time.deltaTime, Space.World);

        Movead();
        Ani();
        jump();
    }
    //캐릭터 회전
    void Movead()
    {
        if (h < 0)
        {
            sp.flipX = true;
        }
        else if (h > 0)
        {
            sp.flipX = false;
        }
    }
    //캐릭터 애니메이션
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
