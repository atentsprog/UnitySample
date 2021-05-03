using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

    }
}
