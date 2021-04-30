using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 애로우 발사하는 스크립트.
/// </summary>
public class Arrow : MonoBehaviour
{
    public GameObject arrowGo;

    public Transform arrowSpawnPosition;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //애로우를 발사.
            Instantiate(arrowGo, arrowSpawnPosition.position, transform.rotation);
            // transform.position // world 월드 포지션.
            // transform.localPosition
        }
    }
}
