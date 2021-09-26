using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class TriggerC : MonoBehaviour
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            print($"자신: {name}({typeof(TriggerC)}), 부딛힘:{other}");
        }
    }
}