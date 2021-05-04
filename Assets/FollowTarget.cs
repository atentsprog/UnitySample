using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public Transform target;

    // 상대적 위치
    private Vector3 offset;

    private void Start()
    {
        offset = target.transform.position - transform.position;
    }

    // Update is called once per frame
    private void Update()
    {
        transform.position = target.transform.position - offset;
    }
}