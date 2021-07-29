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
}

public class DataSaveLoadBy_JSON_PlayerPrefs : MonoBehaviour
{
    public Record record;
    void Start()
    {
        ReadRecord();
    }

    [ContextMenu("ReadRecord")]
    void ReadRecord()
    {
        record = JsonUtility.FromJson<Record>(PlayerPrefs.GetString("record"));
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