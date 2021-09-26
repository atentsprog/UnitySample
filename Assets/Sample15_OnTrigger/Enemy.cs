using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample15_OnTrigger
{
    public class Enemy : MonoBehaviour
    {
        public float vibrate = 0.01f;
        public static List<Enemy> Items = new List<Enemy>();
        private void Awake()
        {
            Items.Add(this);
        }

        void Update()
        {
            if(vibrate > 0)
            {
                transform.Translate(Random.Range(-vibrate, vibrate), 0, 0);
            }
        }

        private void OnDestroy()
        {
            Items.Remove(this);
        }
    }
}