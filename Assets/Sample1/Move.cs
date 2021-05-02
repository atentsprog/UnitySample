using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed = 0.1f;

    private void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        float moveX = 0;
        float moveZ = 0;
        // || -> or
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) moveZ = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) moveZ = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) moveX = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) moveX = 1;
        Vector3 position = transform.position;
        position.x = position.x + moveX * speed;
        position.z = position.z + moveZ * speed;
        transform.position = position;
    }
}