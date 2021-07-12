using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoardMove : MonoBehaviour
{
    public float speed = 5;
    public enum AxisType
    {
        _3D,
        _2D
    }
    public AxisType axisType;
    void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        Vector3 move = new Vector3(0, 0, 0); // Vector3.zero

        // || -> or
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            if (axisType == AxisType._3D)
                move.z = 1;
            else
                move.y = 1;
        }

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            if (axisType == AxisType._3D)
                move.z = -1;
            else
                move.y = -1;
        }

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move.x = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move.x = 1;

        if (move != Vector3.zero)
        {
            move.Normalize();
            move *= speed * Time.deltaTime;
            transform.Translate(move);
        }
    }
}
