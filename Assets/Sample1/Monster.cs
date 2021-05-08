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
        // ȭ�� ������Ʈ �ı���Ŵ
        Destroy(other.gameObject);

        currentHP -= 1; // currentHP  = currentHP - 1
        UpdateHP();

        if (currentHP > 0)
        { // ��� �ִ�. �ǰ� �������.
            StartCoroutine(OnAttacked());
        }
        else
        {
            StartCoroutine(OnDie());
        }
    }

    private void UpdateHP()
    {
        //hp�� ����
        var scale =  hpBar.localScale;
        scale.x = (float)currentHP/maxHP;
        hpBar.localScale = scale;

        // hp�׽�Ʈ ����
        hpText.text = $"{currentHP}/{maxHP}" ;// ����hp/�ִ� hp
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
        // �ǰݴ��ϴ� ���.
        animator.Play("GetHit");

        // ���� ���
        PlaySound(hitAudioClip);


        // �ǰ� �������ϱ� �����ϴ��� ��� ������.
        currentSpeed = 0;
        yield return new WaitForSeconds(attackedMoveStopTime);

        // �ٽ� �̵�.
        currentSpeed = originalSpeed;// �������ǵ�.
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
        // ���� ���
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