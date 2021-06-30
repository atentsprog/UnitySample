using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample8.Collider
{
    public class PlayerCollider : MonoBehaviour
    {

        public float vibrate = 0.01f;
        void Update()
        {
            if (vibrate > 0)
            {
                transform.Translate(Random.Range(-vibrate, vibrate), 0, 0);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AttackCo());
            }
        }

        public GameObject attackCollider;
        int tryAttackCount;
        public float attackColliderActiveTime = 0.2f;
        private IEnumerator AttackCo()
        {
            print(tryAttackCount++ + "번째 공격시도");

            attackCollider.SetActive(true);
            if(attackColliderActiveTime > 0)
                yield return new WaitForSeconds(attackColliderActiveTime);
            else if (attackColliderActiveTime == 0)
                yield return null;
            attackCollider.SetActive(false);
        }
    }
}