using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSaveLoad
{
    public class DataSaveLoadClassType3 : MonoBehaviour
    {
        public FileData<Record> record;
        private void Awake()
        {
            record = new FileData<Record>("Record");
            Debug.LogFormat("stringValue = {0}; bestScore={1}", record.data.stringValue, record.data.bestScore);
        }


        [ContextMenu("SaveRecord")]
        void SaveRecord()
        {
            record.SaveData();
        }

        private void OnDestroy()
        {
            record.SaveData();
        }
    }
}
