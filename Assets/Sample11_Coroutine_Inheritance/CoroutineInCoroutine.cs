using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample11_Coroutine_Inheritance
{
    public class CoroutineInCoroutine : MonoBehaviour
    {
        /// <summary>
        /// Start, OnTriggerEnter, OnCollisionEnter 3개 함수는 반환형이 IEnumerator일경우 코루틴처럼 작동
        /// </summary>
        /// <returns></returns>
        IEnumerator Start()
        {
            yield return StartCoroutine(Co1());
            StartCoroutine(Co2("StartCoroutine으로 호출 1"));
            StartCoroutine(Co1());
            Co2("StartCoroutine 없이 Coroutine 함수 호출");
            StartCoroutine(Co2("StartCoroutine으로 호출 2"));
        }

        IEnumerator Co1()
        {
            for (int i = 0; i < 2; i++)
            {
                yield return Co2(i.ToString());
            }
        }

        private IEnumerator Co2(string log)
        {
            Debug.Log(log);
            yield return null;
            Debug.Log(log + "End");
        }
    }
}