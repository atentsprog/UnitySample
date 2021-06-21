using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 3f;

    public Animator animator;
    public void Awake()
    {
        //Collider tr     = GetComponent<Collider>();
        //Collider[] cols = GetComponents<Collider>();

        //Animator ani1 = GetComponentInChildren<Animator>();
        //Animator ani2 = GetComponentInChildren<Animator>(true);
        //Transform[] ani3 = GetComponentsInChildren<Transform>(false);
    }

    private void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        Vector3 move  = new Vector3(0, 0, 0); // Vector3.zero

        // || -> or
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move.z = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move.z = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move.x= -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move.x = 1;

        Vector3 position = transform.position;
        position.x = position.x + move.x * speed * Time.deltaTime;
        position.z = position.z + move.z * speed * Time.deltaTime;

        move.Normalize();
        transform.position = position;

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Attack01") == false)
        {
            if (move == Vector3.zero)
                animator.Play("Idle_Battle");
            else
                animator.Play("RunForwardBattle");
        }
    }
}