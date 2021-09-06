using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


/// <summary>
/// 파일로 저장하기 때문에 보안에 취약
/// 파일에 저장하므로 느림
/// </summary>
/// <typeparam name="T"></typeparam>
[System.Serializable]
public class FileData<T> where T : new()
{
    public T data = default;
    readonly string key;
    public bool useDebug;

    public FileData(string _key)
    {
        key = _key;
        LoadData();
    }


    string DirPath =>
#if UNITY_EDITOR
        $"{Application.dataPath}/FileData";
#else
        $"{Application.persistentDataPath}";
#endif
    string FilePath => $"{DirPath}/{key}.txt";

    public void LoadData()
    {        
        if(File.Exists(FilePath))
            data = JsonUtility.FromJson<T>(File.ReadAllText(FilePath));

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
            if (Directory.Exists(DirPath) == false)
                Directory.CreateDirectory(DirPath);
            File.WriteAllText(FilePath, json);
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
