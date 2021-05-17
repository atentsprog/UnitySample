using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnGUI_3 : MonoBehaviour
{
    #region 해상도 설정부분
    // 기준이 되는 해상도
    public float width = 500f;
    public float height = 400f;

    // 화면 확대 비율을 구하고, GUI에 적용한다.
    void ChangeGuiScale()
    {
        // 화면 비율 계산
        float _xScale = Screen.width / width;
        float _yScale = Screen.height / height;
        // GUI에 확대/축소 비율 적용
        GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(_xScale, _yScale, 1));
    }
    #endregion 해상도 설정부분 끝

    public string labelString = "내가 레이블이다";
    private void OnGUI()
    {
        ChangeGuiScale(); // GUI에 확대/축소 비율을 정한다. 확인하기 편하게 여기에다가 넣었다.

        GUILayout.BeginHorizontal();
        { 
            GUILayout.Label(labelString);

            if(GUILayout.Button("버튼1"))
            {
                Debug.Log("버튼 눌려졌음");
            }
            GUILayout.Space(40);

            GUILayout.BeginVertical();
            { 
                GUILayout.Button("버튼2");
                GUILayout.Space(20);
                GUILayout.Button("버튼3");
            }
            GUILayout.EndVertical();
        }
        GUILayout.EndHorizontal();
    }
}
