using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class capstone1 : MonoBehaviour
{
    public Animator animator;

    void Start()
    {
        
    }


    void Update()
    {
        if (Input.anyKeyDown)
        {
            animator.Play("attack");
        }
    }
}
