using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Sample14_page
{
    public class PageUISampleItem : MonoBehaviour
    {

        internal void Init(string text)
        {
            GetComponentInChildren<Text>().text = text;
        }
    }
}