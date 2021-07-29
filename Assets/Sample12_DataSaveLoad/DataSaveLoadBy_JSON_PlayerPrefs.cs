using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSaveLoad
{
    [System.Serializable]
    public class Record
    {
        public string stringValue;
        public int bestScore;
        public List<string> names;
        public List<Record> subRecords;
    }

    public class DataSaveLoadBy_JSON_PlayerPrefs : MonoBehaviour
    {
        public Record record;
        void Start()
        {
            ReadRecord();
        }

        private void OnDestroy()
        {
            SaveRecord();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                record.bestScore++;
        }

        [ContextMenu("ReadRecord")]
        void ReadRecord()
        {
            var log = PlayerPrefs.GetString("record");
            print(log);
            record = JsonUtility.FromJson<Record>(log);
            if (record == null)
            {
                Debug.LogWarning("record == null");
                return;
            }
            Debug.LogFormat("stringValue = {0}; bestScore={1}", record.stringValue, record.bestScore);
        }
        [ContextMenu("SaveRecord")]
        void SaveRecord()
        {
            string json = JsonUtility.ToJson(record);

            try
            {
                PlayerPrefs.SetString("record", json);
                Debug.Log("json:" + json);
            }
            catch (System.Exception err)
            {
                Debug.Log("Got: " + err);
            }
        }
    }
}