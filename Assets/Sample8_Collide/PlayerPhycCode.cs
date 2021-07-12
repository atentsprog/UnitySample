using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class PlayerPhycCode : MonoBehaviour
    {
        public float attackRange = 5;
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Attack();
            }
        }

        public LayerMask enemeyLayer;
        private void Attack()
        {
            print("Player hysics.OverlapSphere 공격");
            var result = Physics.OverlapSphere(transform.position, attackRange, enemeyLayer);
            foreach (var item in result)
            {
                print($"Physics.OverlapSphere : {item.name} , 공격 대상");
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, attackRange);
        }
    }
}
