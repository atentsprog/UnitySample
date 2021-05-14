using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(OnGUI_1))]
public class OnGUI_1_InspectorUI : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("해상도 가져오기"))
        {
            OnGUI_1 onGUI_1 = FindObjectOfType<OnGUI_1>();

            //현재 스크린 해상도
            float ratio = (float)Screen.currentResolution.width / Screen.currentResolution.height;

            //onGUI_1에 있는 height값을 현재 비율로 정하자.
            onGUI_1.height = onGUI_1.width / ratio;
        }
    }
}