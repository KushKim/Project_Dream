using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[SerializeField]
public class Player
{
    static public int playerHP = 3;
    public bool infinity;

    public IEnumerator Infinity()
    {
        if (!infinity)
        {
            --playerHP;
            infinity = true;
            yield return new WaitForSeconds(2f);
            infinity = false;
        }
    }
}
public class PlayerMove : MonoBehaviour
{
    [Header("플레이어 수치")]
    public float moveSpeed = 8.5f;
    public float jumpPower = 6f;
    public int jumpCount = 2;


    private Transform Pos;
    private Rigidbody2D rigd;
    private Animator anim;
    private BoxCollider2D box;
    private SpriteRenderer sp;

    private Player player = new Player();

    private AudioManager audioManager;

    private IEnumerator coroutin;
    // Start is called before the first frame update
    void Start()
    {
       Pos = GetComponent<Transform>();
       rigd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
        sp = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        coroutin = Infinity();
    }

    // Update is called once per frame
    void Update()
    {
        if(Player.playerHP <= 0)
        {
            rigd.velocity = Vector2.zero;
            rigd.gravityScale = 0f;
            StopCoroutine(coroutin);
            anim.SetBool("Dead",true);
        }
        else
        {
            Pos.Translate(Vector2.right * moveSpeed * Time.deltaTime);
        }
        if (Pos.position.y < -7)
        {
            rigd.velocity = new Vector2(0.1f, 25f);
            jumpCount = 1;
            if (!player.infinity)
            {
                StartCoroutine(coroutin);
            }
            StartCoroutine(player.Infinity());
            StartCoroutine(EnbleBox());
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
            collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            if (!player.infinity)
            {
                StartCoroutine(coroutin);
            }
            StartCoroutine(player.Infinity());
        }
    }
    public void Jump()
    {
        if (jumpCount > 0)
        {
            audioManager.Play("버튼클릭");
            anim.SetInteger("Jump", jumpCount);
            anim.SetBool("JumpOn", true);
            rigd.velocity = new Vector2(0, jumpPower); // 더블점프 문제로 고정값 넣어둠
            jumpCount--;
        }
    }

    IEnumerator EnbleBox()
    {
        box.enabled = false;
        yield return new WaitForSeconds(1f);
        box.enabled = true;
    }
    IEnumerator Infinity()
    {
        for(int i = 0; i <= 9; i++)
        {
            sp.color = new Color(255, 255, 255, 0);
            yield return new WaitForSeconds(0.1f);
            sp.color = new Color(255, 255, 255, 255);
            yield return new WaitForSeconds(0.1f);
        }
    }
}
