using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
public class Player // 플레이어 상태 클래스
{
    static public int playerHP = 3;
    public bool infinityPlayer;

    public IEnumerator Hit()
    {
        if (!infinityPlayer)
        {
            --playerHP;
            infinityPlayer = true;
            yield return new WaitForSeconds(2f);
            infinityPlayer = false;
        }
    }
}
public class PlayerMove : MonoBehaviour
{
    [Header("움직임 관련 수치")]
    public float moveSpeed = 8.5f;
    public float jumpPower = 6f;
    public int jumpCount = 2;

    private Transform Pos;
    private Rigidbody2D rigd;
    private BoxCollider2D box;
    private SpriteRenderer sp;

    private Animator anim;
    private Player player = new Player();

    void Start()
    {
       Pos = GetComponent<Transform>();
       rigd = GetComponent<Rigidbody2D>();
       anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Pos.Translate(Vector2.right * moveSpeed * Time.deltaTime); // 기본 전진
        if (Pos.position.y < -7) // 떨어진 좌표
        {
            rigd.velocity = new Vector2(0.1f, 20f);
            StartCoroutine(player.Hit());
            StartCoroutine(Infinity());
            StartCoroutine(Boxenble());

        }

        if(Player.playerHP <= 0)
        {

        }
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
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(player.Hit());
            StartCoroutine(Infinity());
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
    public IEnumerator Infinity()
    {
        for (int i = 0; i <= 10; i++)
        {
            sp.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
        }
    }
    public IEnumerator Boxenble()
    {
        box.enabled = false;
        yield return new WaitForSeconds(1f);
        box.enabled = true;
    }
}
