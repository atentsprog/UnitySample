using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smaple8.Collider
{
    public class GameManager : MonoBehaviour
    {
        public List<GameObject> testList;

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
                ToggleActive(0);
            if (Input.GetKeyDown(KeyCode.Alpha2))
                ToggleActive(1);
            if (Input.GetKeyDown(KeyCode.Alpha3))
                ToggleActive(2);
        }

        private void ToggleActive(int activeIndex)
        {
            for (int i = 0; i < testList.Count; i++)
            {
                testList[i].SetActive(i == activeIndex);
            }
        }
    }
}