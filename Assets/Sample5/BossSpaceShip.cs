using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample5.Projectile
{
    public class BossSpaceShip : MonoBehaviour
    {
        public GameObject missleGo;
        public Transform spawnPos;

        public float totalAngle = 360;
        public int count = 36;

        public Vector3 missleSpawnAxis = new Vector3(0, 0, 1);
        public Vector3 missleAddRotation = new Vector3(0, 90, 0);
        public void SpawnMissileFn()
        {
            float angle = 0;
            if (count > 1)
                angle = totalAngle / (count - 1);
            float startAngle = totalAngle * -0.5f;// (count * 0.5f - count);
            for (int i = 0; i < count; i++)
            {
                //var toAngle = transform.forward; // ( 0, 0, 1)
                var eulerAngle = transform.eulerAngles; // (0, 0, 180)
                if (count > 1)
                    eulerAngle += missleSpawnAxis * (startAngle + (angle * i)) + missleAddRotation;

                Quaternion lookRotation = Quaternion.Euler(eulerAngle);
                Instantiate(missleGo, spawnPos.position, lookRotation);
            }
        }
    }
}