using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update


    public Animator Animation;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");

        Animation.Play("GetHit", 0, 0);
       

    }
}
