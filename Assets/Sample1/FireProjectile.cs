using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 애로우 발사하는 스크립트.
/// </summary>
public class FireProjectile : MonoBehaviour
{
    public GameObject arrowGo;

    public Transform arrowSpawnPosition;

    public Animator animator;
    public float interval = 0.5f;

    private IEnumerator Start()
    {
        while (true)
        {
            StartCoroutine(OnFireArrow());
            //Instantiate(arrowGo, arrowSpawnPosition.position, transform.rotation);
            yield return new WaitForSeconds(interval);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartCoroutine(OnFireArrow());  
        }
    }

    public float fireDelay = 0.2f;

    private IEnumerator OnFireArrow()
    {
        //어택엑션 진행
        animator.Play("Attack01", 0, 0);

        //잠시 쉬었다가
        yield return new WaitForSeconds(fireDelay);

        //애로우를 발사.
        Instantiate(arrowGo, arrowSpawnPosition.position, transform.rotation);
    }
}