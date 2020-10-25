using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumping : MonoBehaviour
{
    Rigidbody2D rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    public float bounce = 5.0f;

    void OnCollisionEnter2D(Collision2D other)
    {
        rigid.velocity = Vector2.zero;
    }

    private void FixedUpdate()
    {
        if(Input.GetKeyDown(KeyCode.Space) && rigid.velocity.y == 0)
            {
            rigid.AddForce(Vector2.up * bounce, ForceMode2D.Impulse);
            }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
 
    }
}
