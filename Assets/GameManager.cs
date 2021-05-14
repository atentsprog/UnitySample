using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;

    /// <summary>
    /// 싱글턴 클래스
    /// </summary>
    static public GameManager instance;

    private void Awake()
    {
        // 스타트 함수랑 차이점
        // 실행되는 순서가 더 빠르다.
        // 게임오브젝트가 씬에 나오면 바로실행된다.
        instance = this;
    }

    public TextMeshProUGUI scoreText;
    public void AddScore(int addPoint)
    {
        score += addPoint;
        // UI에 반영.
        // Score : 1,000;
        scoreText.text = "Score : " + $"{score:N0}"; // 아래 줄과 같은 의미
        scoreText.text = "Score : " + score.ToNumber(); // 확장함수 사용 편리함.

        
    }
}
