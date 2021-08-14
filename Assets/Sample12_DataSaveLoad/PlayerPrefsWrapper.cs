using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPrefsWrapper<T>
{
    readonly string key;
    public bool useDebug;
    public PlayerPrefsWrapper(string _key)
    {
        key = _key;
    }

    public T LoadData()
    {
        T record = JsonUtility.FromJson<T>(PlayerPrefs.GetString(key));
        if (record == null)
        {
            Debug.LogWarning("record == null");
            return default(T);
        }

        Log("Load Complete");
        return record;
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(this);

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

[System.Serializable]
public class RankingData : PlayerPrefsWrapper<RankingData>
{
    public RankingData(string key) : base(key)
    {
        var savedData = LoadData();
        if (savedData != null)
        {
            ranking = savedData.ranking;
        }
    }
    public List<int> ranking = new List<int>();
}
