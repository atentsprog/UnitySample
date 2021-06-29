//using DigitalRubyShared;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseEventTest : MonoBehaviour
{
    //public void OnBeginDrag(PointerEventData eventData)
    //{
    //    Debug.Log("OnBeginDrag:" + eventData);
    //}

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log("OnPointerDown:" + eventData);
    //}

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
    }
    void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
    }
    void OnMouseOver()
    {
        Debug.Log("OnMouseOver");
    }
    void OnMouseExit()
    {
        Debug.Log("OnMouseExit");
    }
    void OnMouseEnter()
    {
        Debug.Log("OnMouseEnter");
    }
    void OnMouseDrag()
    {
        Debug.Log("OnMouseDrag");
    }
}
