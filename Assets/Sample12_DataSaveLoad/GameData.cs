using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DataSaveLoad
{
    [Serializable]
    public class PlayerInfo
    {
        public string Name;
        public Color Color;
        // priavate이지만 [SerializeField]의 필드이므로 직렬화 대상에 포함된다.
        [SerializeField] private string comment;

        public string GetColoredName()
        {
            return "<color=#" + ColorUtility.ToHtmlStringRGBA(Color) + ">" + Name + "</color>";
        }
    }

    [CreateAssetMenu(fileName = "New GameData", menuName = "Create GameData", order = 100)]
    public class GameData : ScriptableObject
    {
        // 직렬화 대상
        public List<PlayerInfo> players;
        public int NumberOfRounds;

        public static GameData _instance;
        public static GameData Instance
        {
            get
            {
                return _instance;
            }
        }


        public static void LoadFromJSON(string path)
        {
            if (_instance) DestroyImmediate(_instance);
            // 인스턴스 생성
            _instance = CreateInstance<GameData>();
            // 인스턴스의 직렬화 필드에 JSON 문자열 덮어쓰기
            JsonUtility.FromJsonOverwrite(System.IO.File.ReadAllText(path), _instance);
            _instance.hideFlags = HideFlags.HideAndDontSave;
        }

        public void SaveToJSON(string path)
        {
            Debug.LogFormat("Saving game settings to {0}", path);
            // 이 인스턴스를 JSON 파일로 변환 후 path에 저장
            System.IO.File.WriteAllText(path, JsonUtility.ToJson(this, true));
        }

        public static void InitializeFromDefault(GameData settings)
        {
            // 기존 인스턴스 제거
            if (_instance) DestroyImmediate(_instance);

            // 원본 수정하지 않기 위해서 새로 생성
            _instance = Instantiate(settings);
            _instance.hideFlags = HideFlags.HideAndDontSave;
        }
    }
}