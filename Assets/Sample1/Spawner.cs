using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> spawnPoint;

    private void Start()
    {
        // List에서 사용할 수 있는 다양한 반복문
        //// 방법1.
        //for (int i = 0; i < spawnPoint.Count; i++)
        //{
        //    var x = spawnPoint[i];
        //    Debug.Log(x, x);
        //}

        //// 방법2.
        //foreach (var x in spawnPoint)
        //{
        //    Debug.Log(x, x);
        //}

        // 방법3.
        spawnPoint.ForEach(x => Debug.Log(x, x));

        // 호출할때마다 랜덤한 요소를 리턴하자.
        Transform selected = GetRandomPoint();
    }

    private Transform GetRandomPoint()
    {
        int selectedIndex;

        //// 기본 C#의 랜덤 함수 사용법.
        //System.Random random = new System.Random();
        //selectedIndex = random.Next(0, spawnPoint.Count);

        // UnityEngine에서 제공하는 랜덤 함수 사용법.
        selectedIndex = UnityEngine.Random.Range(0, spawnPoint.Count);

        return spawnPoint[selectedIndex];
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            Debug.Log(GetRandomPoint());
    }
}