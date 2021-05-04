using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private int score;
    public TextMeshProUGUI textMesh;

    private void Awake()
    {
        instance = this;
    }

    internal void AddScore(int getPoint)
    {
        score += getPoint;

        // UI에 숫자 갱신
        textMesh.text = score.ToString();
    }
}