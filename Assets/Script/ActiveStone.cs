using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStone : MonoBehaviour
{
    private Rigidbody2D rigd;

    private Transform player;
    public float distancePos;
    private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        float distance = Vector2.Distance(player.position, transform.position);

        if(distance <= distancePos)
        {
            rigd.gravityScale = 5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 Pos;
        float power;
        if(collision.gameObject.tag.Equals("FallingGround"))
        {
            Pos = new Vector2(Random.Range(-3, 3), 5);
            power = Random.Range(5, 7);
            audioManager.Play("바위");
            rigd.AddForce(Pos * power, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2.75f;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
