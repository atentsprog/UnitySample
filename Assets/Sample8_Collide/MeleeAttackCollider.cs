using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Smaple8.Collider
{
    public class MeleeAttackCollider : MonoBehaviour
    {
        // RigidBody 움직이는거랑 상관없이 감지됨.
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            print("밀리어택 콜라이더에 부딪힘,Trigger " +  other);
        }

        /// <summary>
        /// RigidBody가 있는 쪽이 움직이고 있어야 감지됨.
        /// 움직이지 않고 있다면 랜덤하게 감지됨 <- 사용못함
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            print("밀리어택 콜라이더에 부딪힘, Collision " + collision);
        }
    }
}