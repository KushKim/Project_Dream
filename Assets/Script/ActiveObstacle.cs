using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveObstacle : MonoBehaviour
{
    private bool active;
    public float downSpeed;
    int mask = 1 << 8;
    public GameObject player;
    public float distancePos;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(player.transform.position, transform.position);

        if(distance <= distancePos)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 4f, mask);
            Debug.DrawRay(transform.position, Vector2.down * 4f, Color.red);

            if(hit.collider == null)
            {
                transform.Translate(Vector2.down * downSpeed * Time.deltaTime);
            }
        }
    }
}
