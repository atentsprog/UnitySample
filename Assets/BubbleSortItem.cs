using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BubbleSortItem : MonoBehaviour
{
    int number;
    internal int Number
    {
        set
        {
            number = value;
            GetComponentInChildren<TextMeshPro>().text = number.ToString();
        }
        get
        {
            return number;
        }
    }



    internal void SetColor(Color color)
    {
        Renderer r = GetComponent<MeshRenderer>();
        r.material.color = color;
    }
}
