using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    // Start is called before the first frame update


    public Animator Animation;
    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("�浹");

        //Animation.Play("Die", 0, 0);
        StartCoroutine(DieAndDestroy());

        

    }
    public float interval = 0.5f;
    private IEnumerator DieAndDestroy()
    {
        GetComponent<Collider>().enabled = false;
        enabled = false; //�ڱ� �ڽ��� �������ڴ� (�����̴� �� ���� ���̸�� �ϰ� ����� �ڵ�), ���� ������Ʈ�� falseó���ϸ� ��� ��ũ��Ʈ ������ ����

        Animation.Play("Die", 0, 0);

        yield return new WaitForSeconds(interval);
        Destroy(gameObject);
    }

    public float speed = 3f;
    private void Update()
    {
        transform.Translate(0, 0, speed * Time.deltaTime);
    }
}
