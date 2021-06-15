using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtDirectionMove : MonoBehaviour
{
    public float speed = 5f;

    public Animator animator;

    private void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        Vector3 move = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move.z = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move.z = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move.x = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move.x = 1;

        if (move != Vector3.zero)
        {
            move.Normalize();
            Vector3 initPosition = transform.position;
            Vector3 newPos = initPosition;
            newPos.x = initPosition.x + move.x * speed * Time.deltaTime;
            newPos.z = initPosition.z + move.z * speed * Time.deltaTime;
            transform.position = newPos;
            transform.LookAt(initPosition + move);
        }
        

        if (animator &&  animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") == false)
        {
            if (move != Vector3.zero)
                animator.Play("RunForwardBattle");
            else
                animator.Play("Idle_Battle");
        }
    }
}
