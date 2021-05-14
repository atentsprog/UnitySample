using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class OnGUI_0 : MonoBehaviour
{
	// GUI 클래스 : 고정된 해상도에서 사용.
	void OnGUI()
	{
		float width = 400;
		float height = 200;
		GUI.Label(new Rect(10, 10, width, height), @"GUI 클래스 : 고정된 해상도에서 사용.
큰해상도 작은 해상도에서 보여지는게 달라서 매우 불편, 거의 안쓰임.
Display 해상도를 4K로 변경해보자 -> >_<");

		if (GUI.Button(new Rect(10 + width, 10, width, height), "버튼"))
		{
			Debug.Log("버튼 눌림");
		}

		GUI.Box(new Rect(10 , 10 + height, width, height), "이건 박스 누를 수 없음");

		GUI.Label(new Rect(10 + width, 10 + height , width, height), "이건 레이블 글자보이는게 끝");
	}
}
