
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;


[System.Serializable]
public class EditorPrefsData<T> where T : new()
{
    public T data = default;
    readonly string key;
    public bool useDebug;

    public EditorPrefsData(string _key)
    {
        key = _key;
        LoadData();
    }


    public void LoadData()
    {
        data = JsonUtility.FromJson<T>(EditorPrefs.GetString(key));
        if (data == null)
        {
            Log("record == null");
            data = new T();
            return;
        }

        Log("Load Complete");
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);

        try
        {
            EditorPrefs.SetString(key, json);
            Log("json:" + json);
        }
        catch (System.Exception err)
        {
            Debug.LogError("Got: " + err);
        }
    }
    void Log(string str)
    {
        if (useDebug == false)
            return;

        Debug.Log(str);
    }
}
