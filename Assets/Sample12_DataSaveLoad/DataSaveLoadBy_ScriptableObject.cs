using System;
using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace DataSaveLoad
{
    public class DataSaveLoadBy_ScriptableObject : MonoBehaviour
    {
        public static string SavedSettingsPath
        {
            get
            {
                var path = System.IO.Path.Combine(Application.persistentDataPath, "GameData.Json");
                print(path);
                return path;
            }
        }

        public GameData gameSettingsTemplate = null;
        public GameData devSettings = null;
        void Start()
        {
#if UNITY_EDITOR
            if (devSettings)
            {
                GameData._instance = devSettings;
            }
            else
#endif
            {
                // JSON 파일 유무를 체크
                if (System.IO.File.Exists(SavedSettingsPath))
                    // JSON 파일 로드 및 초기화
                    GameData.LoadFromJSON(SavedSettingsPath);
                else
                    // .asset 파일의 인스턴스로 초기화
                    GameData.InitializeFromDefault(gameSettingsTemplate);
            }

            print("PlayerCount :" + GameData.Instance.players.Count);
            print("NumberOfRounds:" + GameData.Instance.NumberOfRounds);
            GameData.Instance.players.ForEach(x => print(x.GetColoredName()));
        }

        private void OnDestroy()
        {
            Save();
        }

        [ContextMenu("Add Test")]
        void AddUser()
        {
            GameData.Instance.players.Add(new PlayerInfo());
            GameData.Instance.NumberOfRounds++;
        }


        [ContextMenu("Save")]
        public void Save()
        {
            // 해당 경로에 JSON으로 저장한다.
            GameData.Instance.SaveToJSON(SavedSettingsPath);
        }
    }
}