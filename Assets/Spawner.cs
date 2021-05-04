using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> points;
    public GameObject enemyGo;

    public bool isPlaying = true; // 조건알아보기 쉽게 변수로 뺄게요
    public float spawnDelay = 1f;
    void Start()
    {
        //// 리스트 요소 프린트 하기 방식1
        //Debug.LogWarning("리스트 요소 프린트 하기 방식1");
        //for (int i = 0; i < points.Count; i++)
        //{
        //    var item = points[i];
        //    Debug.Log(item);
        //}

        //// 리스트 요소 프린트 하기 방식2
        //Debug.LogWarning("리스트 요소 프린트 하기 방식2");
        //foreach (var item in points)
        //{
        //    Debug.Log(item);
        //}

        //// 리스트 요소 프린트 하기 방식3
        //Debug.LogWarning("리스트 요소 프린트 하기 방식3");
        //points.ForEach(x => Debug.Log(x));

        StartCoroutine( StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        // 게임이 끝날때까지 반복
        while (isPlaying)
        {
            // 랜덤한 위치에 스폰.
            int selectedIndex = Random.Range(0, points.Count); // 0 ~ 3
            //Debug.Log(points[selectedIndex]);

            var selectedTransform = points[selectedIndex];
            Instantiate(enemyGo, selectedTransform.position, selectedTransform.rotation);

            // 잠시 쉬기
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            int selectedIndex = Random.Range(0, points.Count); // 0 ~ 3
            Debug.Log(points[selectedIndex]);

            var selectedTransform = points[selectedIndex];
            Instantiate(enemyGo, selectedTransform.position, selectedTransform.rotation);
        }
    }
}
