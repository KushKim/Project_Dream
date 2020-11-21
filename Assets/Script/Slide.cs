using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public Animator anim;
    public BoxCollider2D box;
    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("Slide",true);
        box.size = new Vector2(11.81f, 9.33f);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetBool("Slide", false);
        box.size = new Vector2(8.330201f, 13.4302f);
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
