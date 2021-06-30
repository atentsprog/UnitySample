using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Sample5.Projectile
{
    public class Projectile : MonoBehaviour
    {
        public float speed = 10;
        public float acceleration = 0;
        [Tooltip("시간당 가속도 적용 비율, acceleration * accelerationCurve 값이 가속도로 적용됨")]
        public AnimationCurve accelerationCurve;

        [Tooltip("유도탄")]
        public float guidedMissileAngle = 0;


        float cumulativeTime = 0;
        void Update()
        {
            if (acceleration != 0)
            {
                float curve = accelerationCurve.Evaluate(cumulativeTime);
                cumulativeTime += Time.deltaTime;
                float addSpeed = curve * acceleration * Time.deltaTime;
                speed += addSpeed;
                //Debug.Log($"speed:{speed}, cumulativeTime:{cumulativeTime}, curve:{curve}");
            }

            if(guidedMissileAngle > 0)
            {
                Vector3 curPosition = transform.position;
                Vector3 targetPos = ProjectileTarget.instance.transform.position;
                if (targetPos.y < curPosition.y) // 플레이어를 지나쳤다면 더이상 유도 되지 않게 하자.
                {
                    Vector3 targetDirection = targetPos - transform.position;

                    if (targetDirection != Vector3.zero)
                        transform.LookAt(curPosition + Vector3.Slerp(transform.forward, targetDirection, guidedMissileAngle));
                }
            }

            //transform.position = transform.position + transform.forward * speed * Time.deltaTime;
            transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);
        }
    }
}