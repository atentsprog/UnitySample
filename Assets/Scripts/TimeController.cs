using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    // Update is called once per frame
    private void Update()
    {
        // Z키 누르면 타일 스케일 느려지도록
        if (Input.GetKeyDown(KeyCode.Z))
            Time.timeScale *= 0.5f;

        // C키 누르면 타임 스케일 빨라지도록
        if (Input.GetKeyDown(KeyCode.C))
            Time.timeScale *= 2f;

        // x키 누르면 타일 스케일 정속도, 정속도일땐 0으로 토글 되도록
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (Time.timeScale == 1)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }
    }
}