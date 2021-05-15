using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
//using TMPro.
public class BubbleSortNewItem : MonoBehaviour
{
    int number;
    internal void SetNumber(int number)
    {
        this.number = number;
        GetComponentInChildren<TextMeshPro>().text = number.ToString();
    }

    internal void ChangeColor(Color color)
    {
        GetComponent<MeshRenderer>().material.color = color;
    }
}
