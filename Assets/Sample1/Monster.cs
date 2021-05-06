using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public Animator animator;

    private void OnTriggerEnter(Collider other)
    {
        // È­»ì ¿ÀºêÁ§Æ® ÆÄ±«½ÃÅ´
        Destroy(other.gameObject);


        StartCoroutine(OnDie());
    }

    public AudioSource dieSound;
    public float delayDieSound = 0.3f;

    public float dieDelay = 1.0f;
    public float speed = -5f;


    private IEnumerator OnDie()
    {
        GameManager.instance.AddScore(100);

        GetComponent<Collider>().enabled = false;
        enabled = false;

        StartCoroutine(PlayDieSound());

        animator.Play("Die");
        yield return new WaitForSeconds(dieDelay);
        Destroy(gameObject);
    }

    private IEnumerator PlayDieSound()
    {
        yield return new WaitForSeconds(delayDieSound);
        dieSound.Play();
    }

    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}