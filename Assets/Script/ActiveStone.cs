﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveStone : MonoBehaviour
{
    private Rigidbody2D rigd;
    // Start is called before the first frame update
    void Start()
    {
        rigd = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Vector2 Pos;
        float power;
        if(collision.gameObject.tag == "FallingGround")
        {
            Pos = new Vector2(Random.Range(-3, 3), 5);
            power = Random.Range(5, 7);
            rigd.AddForce(Pos * power, ForceMode2D.Impulse);
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 2.75f;
            this.gameObject.GetComponent<CircleCollider2D>().enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            rigd.gravityScale = 7f;
        }
    }
}
