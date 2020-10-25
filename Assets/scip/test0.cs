using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test0 : MonoBehaviour
{
    public float parallax;
    public float startpos;
    public float length;
    public float prallaxEffect;

    // Start is called before the first frame update
    void Start()
    {
        parallax = 0f;

        if (GetComponent<SpriteRenderer>())

        length = GetComponent<SpriteRenderer>().bounds.size.x;

        startpos = transform.position.x;

        StartCoroutine(Move());
        
    }

    IEnumerator Move()
    {
        while (true)
        {
            parallax -= prallaxEffect;

            transform.position = new Vector3(startpos + parallax, transform.position.y, transform.position.z);

            if (parallax > length)
            {
                parallax = 0f;
            }else if(parallax < -length)
            {
                parallax = 0f;
            }
            yield return new WaitForFixedUpdate();
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
