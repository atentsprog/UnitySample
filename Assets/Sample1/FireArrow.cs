using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 애로우 발사하는 스크립트.
/// </summary>
public class FireArrow : MonoBehaviour
{
    public GameObject arrowGo;

    public Transform arrowSpawnPosition;

    public Animator animator;

    private void Update()
    {
        if (canFire)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                StartCoroutine(OnFireArrow());
            }
        }
    }

    public float fireDelay = 0.2f;

    private bool canFire = true;

    private IEnumerator OnFireArrow()
    {
        canFire = false;

        //어택엑션 진행
        animator.enabled = false;
        animator.enabled = true;
        animator.Play("Attack01");

        // 잠시 쉬었다가
        yield return new WaitForSeconds(fireDelay);

        //애로우를 발사.
        Instantiate(arrowGo, arrowSpawnPosition.position, transform.rotation);

        canFire = true;
    }
}