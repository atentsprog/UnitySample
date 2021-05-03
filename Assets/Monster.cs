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
        Debug.Log("충돌");

        //Animation.Play("Die", 0, 0);
        StartCoroutine(DieAndDestroy());

        

    }
    public float interval = 0.5f;
    private IEnumerator DieAndDestroy()
    {
        GetComponent<Collider>().enabled = false;
        enabled = false; //자기 자신을 꺼버리겠다 (움직이는 걸 끄고 다이모션 하게 만드는 코드), 게임 오브젝트를 false처리하면 모든 스크립트 동작이 멈춤

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
