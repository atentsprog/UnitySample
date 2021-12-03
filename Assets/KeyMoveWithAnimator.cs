using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyMoveWithAnimator : MonoBehaviour
{
    public float speed = 5;
    public enum AxisType
    {
        _3D,
        _2D
    }
    public AxisType axisType;

    Animator animator;
    int speedHash;
    private void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        speedHash = Animator.StringToHash("speed");
    }
    void Update()
    {
        Vector3 move = new Vector3(0, 0, 0);

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
            Vector3 relateMove;
            relateMove = Camera.main.transform.forward * move.z; // 0, -1, 0
            relateMove += Camera.main.transform.right * move.x;
            relateMove.y = 0;
            relateMove.Normalize();

            transform.Translate(relateMove * speed * Time.deltaTime, Space.World);

            transform.forward = relateMove;
        }

        animator.SetFloat(speedHash, move.sqrMagnitude);
    }
}
