using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenMove : MonoBehaviour
{
    // 큐브에 달린 스크립트
    // 스피어 위치로 이동시키자.

    public Transform target;
    public float duration = 0.5f;
    public Ease ease = Ease.Flash;
    Vector3 originalPos;
    private Vector3 originalScale;
    private Vector3 targetScale;

    private void Start()
    {
        originalPos = transform.position;
        originalScale = transform.localScale;
        targetScale = originalScale * 3;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))        
        {
            ease--;
            if (ease == Ease.Unset)
                ease = Ease.InOutFlash;
            RestartAnimation();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))       
        {
            ease++;
            if (ease == Ease.INTERNAL_Zero)
                ease = Ease.Linear;
            RestartAnimation();
        }
    }

    private void RestartAnimation()
    {
        TweenScaleFn();
        //TweenMoveFn();
    }

    private void TweenScaleFn()
    {
        transform.localScale = originalScale;

        // 타겟 위치로 트위닝
        //Vector3 targetScale = target.localScale;
        transform.DOScale(targetScale, duration).SetEase(ease);
    }

    private void TweenMoveFn()
    {
        // 원래 위치로 이동.
        transform.position = originalPos;

        // 타겟 위치로 트위닝
        Vector3 targetPos = target.position;
        transform.DOMove(targetPos, duration).SetEase(ease);
    }
}
