using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DataSaveLoad
{
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
        public Record ranking = new Record();
    }


    public class DataSaveLoadClassType2 : MonoBehaviour
    {
        public RankingData record;

        private void Awake()
        {
            record = new RankingData("PlayerPrefsWrapperRecord");
            Debug.LogFormat("stringValue = {0}; bestScore={1}", record.ranking.stringValue, record.ranking.bestScore);
        }

        [ContextMenu("SaveRecord")]
        void SaveRecord()
        {
            record.SaveData();
        }

    }
}
