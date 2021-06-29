using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Smaple8.Collider
{
    public class PlayerCollider : MonoBehaviour
    {
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                Attack();
            }
        }

        public GameObject attackCollider;
        private void Attack()
        {
            attackCollider.SetActive(true);
            attackCollider.SetActive(false);
        }
    }
}