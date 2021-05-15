using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버튼과 여백 샘플.
/// </summary>
[ExecuteInEditMode]
public class OnGUI_1 : MonoBehaviour
{
	// 기준이 되는 해상도
	public float width = 1024f;
	public float height = 768f;

	// 화면 확대 비율을 구하고, GUI에 적용한다.
	void ChangeGuiScale()
	{
		// 화면 비율 계산
		float _xScale = Screen.width / width;
		float _yScale = Screen.height / height;
		// GUI에 확대/축소 비율 적용
		GUI.matrix = Matrix4x4.TRS(Vector3.zero, Quaternion.identity, new Vector3(_xScale, _yScale, 1));
	}

	public bool leftFlexibleSpace = false;
	public bool rightFlexibleSpace = false;
	public bool topFlexibleSpace = false;
	public bool bottomFlexibleSpace = false;

	// GUI 화면을 그린다.
	void OnGUI()
	{
		ChangeGuiScale(); // GUI에 확대/축소 비율을 정한다. 확인하기 편하게 여기에다가 넣었다.

		Rect _rcScreen = new Rect(0, 0, width, height); // UI 그리기 시작  - Area크기는 미리 정한 해상도만큼.
		GUILayout.BeginArea(_rcScreen); // Area를 설정하고, AutoLayout으로 UI 요소를 테스트용으로 아무거나 넣었다.
		{
			if (topFlexibleSpace)
				GUILayout.FlexibleSpace(); // 빈 여백 만들기.

			GUILayout.BeginHorizontal();
			{
				if (leftFlexibleSpace)
					GUILayout.FlexibleSpace(); // 빈 여백 만들기.

				if (GUILayout.Button("왼쪽 여백 생성"))
				{
					leftFlexibleSpace = !leftFlexibleSpace;
				}

				GUILayout.BeginVertical();
				{
					if (GUILayout.Button("위 여백 생성"))
					{
						topFlexibleSpace = !topFlexibleSpace;
					}

					if (GUILayout.Button("아래 여백 생성"))
					{
						bottomFlexibleSpace = !bottomFlexibleSpace;
					}

					GUILayout.Box("Vertical 1");
					GUILayout.Box("Vertical 2");
				}
				GUILayout.EndVertical();

				GUILayout.BeginHorizontal();
				{
					GUILayout.Box("Horizontal1"); 
					GUILayout.Box("Horizontal2");
				}
				GUILayout.EndHorizontal();

				if (GUILayout.Button("오른쪽 여백 생성"))
				{
					rightFlexibleSpace = !rightFlexibleSpace;
				}

				if(rightFlexibleSpace)
					GUILayout.FlexibleSpace(); // 빈 여백 만들기.
			}
			GUILayout.EndHorizontal();

			if (bottomFlexibleSpace)
				GUILayout.FlexibleSpace(); // 빈 여백 만들기.
		}
		GUILayout.EndArea();
	}
}
