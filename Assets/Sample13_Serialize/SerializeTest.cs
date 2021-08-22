using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SerializeTest : MonoBehaviour
{

    public SerializableDictionary<string, string> strStrDic = new SerializableDictionary<string, string>();
    public SerializableDictionary<string, int> inventoryData = new SerializableDictionary<string, int>();
    public SerializableDictionary<int, int> intIntDic = new SerializableDictionary<int, int>();

    string inventoryDataKey = "inventoryData";
    void Start()
    {
        strStrDic["string"] = "test";
        inventoryData["str1"] = 1;
        intIntDic[1] = 2;
    }

    [ContextMenu("Save")]
    private void SaveInvenData()
    {
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
        inventoryData = JsonUtility.FromJson<SerializableDictionary<string, int>>(PlayerPrefs.GetString(inventoryDataKey));
        if (inventoryData == null)
        {
            Debug.LogWarning("record == null");
            return;
        }
    }
}
