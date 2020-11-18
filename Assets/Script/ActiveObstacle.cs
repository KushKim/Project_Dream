using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObstacle : MonoBehaviour
{
    private bool active;
    public float downSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int mask = 1 << 8;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 4f, mask);
        Debug.DrawRay(transform.position, Vector2.down * 4f, Color.red);
        if (active)
        {
            if(hit.collider == null)
            {
                transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            active = true;
        }
    }
}
