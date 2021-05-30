using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpaceShip : MonoBehaviour
{
    public MoveLocal missleGo;

    public float totalAngle = 360;
    public int count = 36;

    public Vector3 missleSpawnAxis = new Vector3(0, 0, 1);
    public Vector3 missleMoveAxis = new Vector3(0, 1, 0);
    public void SpawnMissileFn()
    {
        float angle = 0;
        if(count > 1)
            angle = totalAngle / (count - 1);
        float startAngle = totalAngle * -0.5f;// (count * 0.5f - count);
        for (int i = 0; i < count; i++)
        {
            //var toAngle = transform.forward; // ( 0, 0, 1)
            var eulerAngle = transform.eulerAngles; // (0, 0, 180)
            if(count > 1)
                eulerAngle += missleSpawnAxis * (startAngle + (angle * i));

            Quaternion lookRotation = Quaternion.Euler(eulerAngle);
            MoveLocal moveLocal = Instantiate(missleGo, transform.position, lookRotation);

            moveLocal.moveAxis = missleMoveAxis;
        }
    }
}
