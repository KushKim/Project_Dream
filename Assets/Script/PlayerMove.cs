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
    public int playerHp;
    private bool hitObstacle;
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
        if(playerHp <= 0)
        {

        }

        Pos.Translate(Vector2.right * moveSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag.Equals("Ground"))
        {
            jumpCount = 2;
            anim.SetBool("JumpOn", false);
        }

        if (collision.gameObject.tag.Equals("Obstacle"))
        {
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (!hitObstacle)
            {
                playerHp--;
                StartCoroutine(Delay());
            }
        }
    }
    public void Jump()
    {
        if (jumpCount > 0)
        {
            anim.SetInteger("Jump", jumpCount);
            anim.SetBool("JumpOn", true);
            rigd.velocity = new Vector2(0, jumpPower); // 더블점프 문제로 고정값 넣어둠
            jumpCount--;
        }
    }

    IEnumerator Delay()
    {
        hitObstacle = true;
        yield return new WaitForSeconds(2f);
        hitObstacle = false;
    }
}
