using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Monster : MonoBehaviour
{
    public Animator animator;

    public Transform hpBar;
    public TextMeshPro hpText;
    public int currentHP;
    public int maxHP = 3;

    private void Start()
    {
        currentHP = maxHP;
        currentSpeed = originalSpeed;
        UpdateHP();
    }

    private void OnTriggerEnter(Collider other)
    {
        // 화살 오브젝트 파괴시킴
        Destroy(other.gameObject);

        currentHP -= 1; // currentHP  = currentHP - 1
        UpdateHP();

        if (currentHP > 0)
        { // 살아 있다. 피격 모션하자.
            StartCoroutine(OnAttacked());
        }
        else
        {
            StartCoroutine(OnDie());
        }
    }

    private void UpdateHP()
    {
        //hp바 갱신
        var scale =  hpBar.localScale;
        scale.x = (float)currentHP/maxHP;
        hpBar.localScale = scale;

        // hp테스트 갱신
        hpText.text = $"{currentHP}/{maxHP}" ;// 현재hp/최대 hp
    }

    public AudioSource audioSource;
    public float delayDieSound = 0.3f;

    public float currentSpeed;
    public float originalSpeed = 5f;

    public float attackedMoveStopTime = 0.4f;
    public AudioClip hitAudioClip;
    public AudioClip dieAudioClip;
    public float dieDelay = 1.0f;
    public float hitDelay = 0.3f;

    IEnumerator OnAttacked()
    {
        // 피격당하는 모션.
        animator.Play("GetHit");

        // 사운드 재생
        PlaySound(hitAudioClip);


        // 피격 당했으니깐 전진하던걸 잠시 멈추자.
        currentSpeed = 0;
        yield return new WaitForSeconds(attackedMoveStopTime);

        // 다시 이동.
        currentSpeed = originalSpeed;// 원래스피드.
    }


    private IEnumerator OnDie()
    {
        GameManager.instance.AddScore(100);

        GetComponent<Collider>().enabled = false;
        enabled = false;

        PlaySound(dieAudioClip);

        animator.Play("Die");
        yield return new WaitForSeconds(dieDelay);
        Destroy(gameObject);
    }
    private void PlaySound(AudioClip audioClip)
    {
        // 사운드 재생
        audioSource.PlayOneShot(audioClip);
    }

    //private IEnumerator PlayDieSound()
    //{
    //    yield return new WaitForSeconds(delayDieSound);
    //    dieSound.Play();
    //}

    private void Update()
    {
        transform.Translate(0, 0, currentSpeed * Time.deltaTime);
    }
}