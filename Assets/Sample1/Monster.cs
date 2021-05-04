using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        StartCoroutine(OnDie());
    }

    public float dieDelay = 1.0f;
    public float speed = -5f;

    private IEnumerator OnDie()
    {
        GameManager.instance.score += 100;

        GetComponent<Collider>().enabled = false;
        enabled = false;
        animator.Play("Die");
        yield return new WaitForSeconds(dieDelay);
        Destroy(gameObject);
    }

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}