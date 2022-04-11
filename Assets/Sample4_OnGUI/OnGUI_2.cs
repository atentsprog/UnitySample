using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 버튼과 여백 샘플.
/// </summary>
[ExecuteInEditMode]
public class OnGUI_2 : MonoBehaviour
{
    #region 해상도 설정부분
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
    #endregion 해상도 설정부분 끝

    public string textFieldString = "TextField:한줄만 입력가능";
	[TextArea]
	public string textAreaString = "TextArea:자동으로 줄 바뀜";
	public int tabIndex;
	string[] tabSubject = { "Button,Label", "TextField, TextArea", "CheckBox, Toggle", "기타" };

	public Texture texture;
	private bool ToggleTextEnable
	{
		get { return toggleTextEnable; }
		set
		{
			if (toggleTextEnable == value)
				return;

			Debug.Log($"toggleText 값이 변했다. {toggleTextEnable} -> {value}");
			toggleTextEnable = value;
		}
	}
	bool toggleTextEnable = false;
	private bool toggleImageEnable = false;

	// GUI 화면을 그린다.
	void OnGUI()
	{
		ChangeGuiScale(); // GUI에 확대/축소 비율을 정한다. 확인하기 편하게 여기에다가 넣었다.

		Rect _rcScreen = new Rect(0, 0, width, height); // UI 그리기 시작  - Area크기는 미리 정한 해상도만큼.
		GUILayout.BeginArea(_rcScreen); // Area를 설정하고, AutoLayout으로 UI 요소를 테스트용으로 아무거나 넣었다.
		{
			tabIndex = GUILayout.SelectionGrid(tabIndex, tabSubject, 2);

			GUILayout.FlexibleSpace();		// 왼쪽 여백
			GUILayout.BeginHorizontal();	// 가로 배치 시작
			GUILayout.FlexibleSpace();      // 위쪽 여백
			switch (tabIndex)
			{
				case 0:
					OnGUI_Button_Label();
					break;
				case 1:
					OnGUI_TextFieldTextArea();
					break;
				case 2:
					OnGUI_Toggle();
					break;
				case 3:
					OnGUI_Etcetera();
					break;
			}
			GUILayout.FlexibleSpace();	// 아래쪽 여백
			GUILayout.EndHorizontal();	// 가로 배치 끝
			GUILayout.FlexibleSpace();	// 오른쪽 여백
		}
		GUILayout.EndArea();
	}


	public string labelText = "Label";
    private string passwordToEdit = "MyPassword";
    private Vector2 scrollPosition;

    private void OnGUI_Button_Label()
	{
		GUILayout.BeginVertical();
		{
			GUILayout.BeginHorizontal(); // 첫번째줄 시작
			{
				GUILayout.Label(labelText);
				GUILayout.Label(texture,GUILayout.Width(texture.width), GUILayout.Height(texture.height));
				if (GUILayout.Button("Button"))
				{
					Debug.Log("글자 버튼 누름");
				}
				if (GUILayout.Button(texture))
				{
					Debug.Log("texture 버튼 누름");
				}
			}
			GUILayout.EndHorizontal();// 첫번째줄 끝

			GUILayout.BeginVertical(); // 두번째줄 시작
			{
				passwordToEdit = GUILayout.PasswordField(passwordToEdit, "*"[0], 25);
			}
			GUILayout.EndVertical();// 두번째줄 끝
		}
		GUILayout.EndVertical(); 

	}

	private void OnGUI_TextFieldTextArea()
	{
		scrollPosition = GUILayout.BeginScrollView(scrollPosition, GUILayout.Height(120));
		{ 
			textFieldString = GUILayout.TextField(textFieldString);
			textAreaString = GUILayout.TextArea(textAreaString);
		}
		GUILayout.EndScrollView();
	}

	void WriteLine(Action fn)
	{
		GUILayout.BeginVertical();
		GUILayout.BeginHorizontal();
		fn();
		GUILayout.EndHorizontal();
		GUILayout.EndVertical();
	}
	private void OnGUI_Toggle()
	{
		GUILayout.EndHorizontal();

		WriteLine(() => {
			GUILayout.Box("체크 박스");
		});

		WriteLine(() => {
			// 토글만 표시
			toggleImageEnable = GUILayout.Toggle(toggleImageEnable, texture, GUILayout.Width(texture.width * 0.4f), GUILayout.Height(texture.height * 0.4f));

			// 속성을 통해서 기능(로그 출력)실행
			ToggleTextEnable = GUILayout.Toggle(ToggleTextEnable, "A Toggle text"); 
		});

		WriteLine(() => {
			GUILayout.Box("토글 - 하나만 선택 가능");
		});

		WriteLine(() => {
			ToggleList(ref toggleSelected, new GUIContent[]
			{ 
				new GUIContent("토글 버튼1")
				, new GUIContent("토글 버튼2")
				, new GUIContent("토글 버튼3")
			}
			, ()=> {
				Debug.Log($"인덱스 {toggleSelected} 선택되었다");
			});
		});


		GUILayout.BeginHorizontal();
	}
	int toggleSelected = 0;
	public int ToggleList(ref int selected, GUIContent[] items, Action fn)
	{
		selected = selected < 0 ? 0 : selected >= items.Length ? items.Length - 1 : selected;

		int oldSelected = selected;
		GUILayout.BeginVertical();
		for (int i = 0; i < items.Length; i++)
		{
			bool before = oldSelected == i;
			bool after = GUILayout.Toggle(before, items[i]);
			bool change = before != after;
			if (change & after)
			{
				selected = i;
				fn();
			}
		}
		GUILayout.EndVertical();

		// Return the currently selected item's index
		return selected;
	}

	private void OnGUI_Etcetera()
	{
		GUILayout.Box("공간 뛰우기", GUILayout.Height(50), GUILayout.Width(100));
		GUILayout.Space(10);
		GUILayout.Box("<-Space 10");
		GUILayout.Space(100);
		GUILayout.Box("<-Space 100");
	}
}
