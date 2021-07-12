using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class PlayerOnlyCode : MonoBehaviour
    {
        public float attackRange = 5;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
        private void Attack()
        {
            print("Player OnlyCode 공격");
            foreach (var item in Enemy.Items)
            {
                var distance = Vector3.Distance(transform.position, item.transform.position);
                if (distance < attackRange)
                {
                    print($"Vector3.Distance : {item.name} 거리:{distance}, 공격 대상");
                }
            }
        }
    }
}