using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class TriggerB : MonoBehaviour
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            print($"자신: {name}({typeof(TriggerB)}), 부딛힘:{other}");
        }
    }
}