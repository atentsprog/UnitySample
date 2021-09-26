using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class TriggerA : MonoBehaviour
    {
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            print($"자신: {name}({typeof(TriggerA)}), 부딛힘:{other}");
        }
    }
}