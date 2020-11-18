using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Slide : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
{
    public Animator anim;
    public void OnPointerDown(PointerEventData eventData)
    {
        anim.SetBool("Slide",true);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        anim.SetBool("Slide", false);
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
