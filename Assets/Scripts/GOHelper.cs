using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GOHelper : MonoBehaviour
{
    /// <summary>
    /// 0���� Ŭ��� Ȱ��ȭ�� destroyTime�� ���� �ı���
    /// </summary>
    public float destroyTime;

    public bool OnEnableLog;
    public bool OnDisableLog;
    public bool OnDestroyLog;
    public bool StopOnLog; // �α� �߻��� ����

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