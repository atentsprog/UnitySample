using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    /// <summary>
    /// �̱��� Ŭ����
    /// </summary>
    static public GameManager instance;

    private void Awake()
    {
        // ��ŸƮ �Լ��� ������
        // ����Ǵ� ������ �� ������.
        // ���ӿ�����Ʈ�� ���� ������ �ٷν���ȴ�.
        instance = this;
    }
}
