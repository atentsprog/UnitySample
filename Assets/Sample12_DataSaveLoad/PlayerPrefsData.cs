
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Data<T>
{
    public T data;
}


[System.Serializable]
public class PlayerPrefsData<T>
{
    public T data = default(T);
    readonly string key;
    public bool useDebug;

    public PlayerPrefsData(string _key)
    {
        key = _key;
        LoadData();
    }


    public void LoadData()
    {
        data = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        if (data == null)
        {
            Log("record == null");
            return;
        }

        Log("Load Complete");
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(data);

        try
        {
            PlayerPrefs.SetString(key, json);
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
