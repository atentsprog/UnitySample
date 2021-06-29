using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardMove : MonoBehaviour
{
    public float speed = 5;
    // Update is called once per frame
    void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        Vector3 move = new Vector3(0, 0, 0); // Vector3.zero

        // || -> or
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move.z = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move.z = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move.x = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move.x = 1;

        Vector3 position = transform.position;
        position.x = position.x + move.x * speed * Time.deltaTime;
        position.z = position.z + move.z * speed * Time.deltaTime;

        move.Normalize();
        transform.position = position;
    }
}
