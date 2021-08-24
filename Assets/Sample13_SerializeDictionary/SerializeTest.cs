using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//출처 http://wehappyfamily.blogspot.com/2017/12/unity-local-db-c-jsonutility.html
[System.Serializable]
public class SerializationList<T>
{
    [SerializeField]
    List<T> target;
    public List<T> ToList() { return target; }

    public SerializationList(List<T> target)
    {
        this.target = target;
    }
}

public class SerializeTest : MonoBehaviour
{
    public SerializableDictionary<string, string> strStrDic = new SerializableDictionary<string, string>();
    public SerializableDictionary<string, int> inventoryData = new SerializableDictionary<string, int>();
    public SerializableDictionary<int, int> intIntDic = new SerializableDictionary<int, int>();
    public List<int> intList = new List<int>();

    string inventoryDataKey = "inventoryData";
    string intListKey = "intList";
    void Start()
    {
        strStrDic["string"] = "test";
        inventoryData["str1"] = 1;
        intIntDic[1] = 2;
    }

    [ContextMenu("Save")]
    private void SaveInvenData()
    {
        string intJson = JsonUtility.ToJson(intList);
        string intSerializationJson = JsonUtility.ToJson(new SerializationList<int>(intList));
        print(intJson + "" + intJson.Length);
        print(intSerializationJson + "" + intSerializationJson.Length);
        PlayerPrefs.SetString(intListKey, intSerializationJson);


        string json = JsonUtility.ToJson(inventoryData);
        try
        {
            PlayerPrefs.SetString(inventoryDataKey, json);
        }
        catch (System.Exception err)
        {
            Debug.Log($"Error:{err}");
        }
    }

    [ContextMenu("Load")]
    private void LoadInvenData()
    {
        intList = JsonUtility.FromJson<SerializationList<int>>(PlayerPrefs.GetString(intListKey)).ToList();


        inventoryData = JsonUtility.FromJson<SerializableDictionary<string, int>>(PlayerPrefs.GetString(inventoryDataKey));
        if (inventoryData == null)
        {
            Debug.LogWarning("record == null");
            return;
        }
    }
}
