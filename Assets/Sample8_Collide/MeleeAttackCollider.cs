using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Sample8.Collider
{
    /// <summary>
    /// 물리계열로 충돌 체크 하려면 충돌 콜라이더 켜져 있는 프레임이 3프레임 이상이면 안됨
    /// 이유 : 충돌했던 대상이 다시 충돌 할 수도 있음.
    /// 1Frame : 충돌
    /// 2Frame : 이탈
    /// 3Frame : 재집입(충돌)
    /// </summary>
    public class MeleeAttackCollider : MonoBehaviour
    {
        // RigidBody 움직이는거랑 상관없이 감지됨.
        // 1frame이라도 게임오브젝트가 켜져 있으면 감지됨.
        private void OnTriggerEnter(UnityEngine.Collider other)
        {
            print("밀리어택 콜라이더에 부딪힘,Trigger " +  other);
        }

        /// <summary>
        /// RigidBody가 있는 쪽이 움직이고 있어야 감지됨.
        /// -> 진동하고 있다면 여러번 부딪힐 수 있음.
        /// 움직이지 않고 있다면 랜덤하게 감지됨 <- 사용못함
        /// 무기에 달아뒀다면 무기가 물리적 충돌에 의해서 엉뚱한 곳으로 이동할 수 있음.
        /// </summary>
        /// <param name="collision"></param>
        private void OnCollisionEnter(Collision collision)
        {
            print("밀리어택 콜라이더에 부딪힘, Collision " + collision.transform.name);
        }
    }
}