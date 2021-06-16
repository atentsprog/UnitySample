using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOHelper : MonoBehaviour
{
    /// <summary>
    /// 0보다 클경우 활성화후 destroyTime초 이후 파괴됨
    /// </summary>
    public float destroyTime;

    public bool OnEnableLog;
    public bool OnDisableLog;
    public bool OnDestroyLog;
    public bool StopOnLog; // 로그 발생시 멈춤

    private void Start()
    {
        if (destroyTime > 0)
            Destroy(gameObject, destroyTime);
    }

    private void OnEnable()
    {
        if (OnDestroyLog)
            WriteLog();
    }

    private void OnDisable()
    {
        if (OnDisableLog)
            WriteLog();
    }

    private void OnDestroy()
    {
        if (OnDestroyLog)
            WriteLog();
    }

    private void WriteLog()
    {
        if (StopOnLog)
            Debug.LogError(transform.GetPath(), transform);
        else
            Debug.Log(transform.GetPath());
    }
}