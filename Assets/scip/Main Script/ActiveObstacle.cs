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
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 0.1f);
        Debug.DrawRay(transform.position, Vector2.down * 4f, Color.red);
        if (active)
        {
            if(hit.collider != null)
            {
                Debug.Log(hit.collider.name);
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
