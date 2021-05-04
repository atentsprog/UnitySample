using System.Collections;
using System.Collections.Generic;
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
}
