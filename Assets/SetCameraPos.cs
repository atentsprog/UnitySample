using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCameraPos : MonoBehaviour
{
    void Start()
    {
        Camera.main.transform.position = transform.position;
    } 
}
