using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player0 : MonoBehaviour
{
    float movespeed = 4.0f;
    //float spinspeed = 180f;
    float jumpspeed = 4.0f;
    float Jumptime = 0.4f;
    float Jumpstarttime;
    bool bJump = false;
    //bool bJumpup = false;
    bool bDowm = false;

    //Transform target
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos_old = transform.position;

        //이동
        if (Input.GetKey(KeyCode.RightArrow))
            transform.position += Vector3.right * movespeed * Time.deltaTime;
        if (Input.GetKey(KeyCode.LeftArrow))
            transform.position += Vector3.left * movespeed * Time.deltaTime;

        //check 점프
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!bJump && !bDowm)
            {
                Jumpstarttime = Time.time;
                bJump = true;
            }
        }

        //check movedown
        if (!bJump)
        {
            RaycastHit hit;
            GameObject target = null;
            if (true == Physics.Raycast(transform.position, -transform.up, out hit, 0.1f)) // 일정 거리 레이캐스트
            {
                target = hit.collider.gameObject; //있으면 오브젝트를 저장한다.
                //print("target" + target.name);
            }
            if (target != null && target.name.Contains("ground")) { bDowm = false; }
            else { bDowm = true; }
        }
        if (bJump)
        {
            transform.position += Vector3.up * jumpspeed * Time.deltaTime;
            if (Time.time >= Jumpstarttime + Jumptime) { bJump = false; }
        }
        if (bDowm)
        {
            transform.position += Vector3.down * jumpspeed * Time.deltaTime;
        }

        //방향전환
        //vector3 dir = transform.position - pos_old; dir.y = 0;
        //transform.pos = transform.position + dir * 1;
        //transform.LookAt(pos);
    }

    void OnTriggerEnter(Collider other) //충돌판정에는 양쪽다 collider 필요, 한쪽엔 rigbody
    {
        //print("player OnTriggerEnter" + other.name);

        if (other.name.Contains("ground") && !bJump) bDowm= false;
    }

    void OnCollisionEnter(Collision collision) //충돌지점 계산 //물리연산
    {
        //print("player OnCollisionEnter" + other.transform.name);
    }
}

