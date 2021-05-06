using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    public TextMeshProUGUI scoreText;
    public void AddScore(int addPoint)
    {
        score += addPoint;
        // UI�� �ݿ�.
        // Score : 1,000;
        scoreText.text = "Score : " + $"{score:N0}"; // �Ʒ� �ٰ� ���� �ǹ�
        scoreText.text = "Score : " + score.ToNumber(); // Ȯ���Լ� ��� ����.

        
    }
}
