using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using spine.Unity; //스파인사용

public class capstone0 : MonoBehaviour
{
    public float movespeed = 5f; //에디터에서 변경
    public float jumppower = 6f;
    public float rotatespeed = Mathf.PI * 4f;

    //Rigidbody rigidbody;

    Vector3 movemont;
    float h_move;
    //float v_move;
    //bool isjumping = false;
    bool isground = true;

    //public Transform firepoint;
    //public rigidbody rocket;
    //float rocket_speed = 10f;

    public Animator animator; //애니메이션
    string anim_name = "idle";
    //public skeletonGraphic skeleton2D; //2d용 스파인 컴포넌트
    //public skeletonAnimation skeleton3D;

    void Awake()
    {
        //rigidbody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        
    }
    public void Animationset(string _name, bool _loop = true)
    {
        if (anim_name == _name) return;
        anim_name = _name;

        //if (skeleton3D != null)
        {
            //skeleton3D.AnimationState.SetAnimation(0, _name, _loop);
        }
        if (animator != null)
        {
            animator.Play(_name);
        }
    }

    void Update()
    {
        h_move = Input.GetAxisRaw("Horizontal");
        //v_move = input.GetAxisRaw("Vertical");

        if (Input.GetButtonDown("Jump"))
        {
            if (isground) Jump();
        }

        //if(input.GetButtonDown("Firel"))
        //{
        //  //FireRocket();
        //}
    }

    void FixedUpdate()
    {
        Run();
        Turn();
    }

    void Run()
    {
        //movemont.Set(h_move, 0, v_move);
        movemont = movemont.normalized * movespeed * Time.deltaTime;

        //rigidbody.MovePosition(transform.position + movemont);

        if (isground)
        {
            if (movemont == Vector3.zero) Animationset("idle");
            else                          Animationset("run");
        }
    }

    void Turn()
    {
        //if (h_move == 0 && v_move == 0) return;
        //rigidbody.rotation = Quaternion.LookRotation(movemont);
        //Quaternion new Rotation = Quaternion.LookRotation(movemont);
        //rigidbody.rotation = Quaternion.Slerp(rigidbody.roataion, newRotation, rotatespeed * Time.deltaTime);
    }

    void Jump()
    {
        //print("Jump");
        //isjumping = true;
        isground = false;
        //rigidbody.AddForce(Vector3.up * jumppower, ForceMode.Impulse);

        Animationset("jump", false);
    }

    private void OnCollisionEnter(Collision other)
    {
        //print("player OnCollisionEnter " + other.transform.name);
        if (other.transform.name.Contains("ground"))
        {
            isground = true;
            //isjumping = false;
            Animationset("idle");
        }
    }
}
