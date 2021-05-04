using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> points;
    public GameObject enemyGo;

    public bool isPlaying = true; // ���Ǿ˾ƺ��� ���� ������ ���Կ�
    public float spawnDelay = 1f;
    void Start()
    {
        //// ����Ʈ ��� ����Ʈ �ϱ� ���1
        //Debug.LogWarning("����Ʈ ��� ����Ʈ �ϱ� ���1");
        //for (int i = 0; i < points.Count; i++)
        //{
        //    var item = points[i];
        //    Debug.Log(item);
        //}

        //// ����Ʈ ��� ����Ʈ �ϱ� ���2
        //Debug.LogWarning("����Ʈ ��� ����Ʈ �ϱ� ���2");
        //foreach (var item in points)
        //{
        //    Debug.Log(item);
        //}

        //// ����Ʈ ��� ����Ʈ �ϱ� ���3
        //Debug.LogWarning("����Ʈ ��� ����Ʈ �ϱ� ���3");
        //points.ForEach(x => Debug.Log(x));

        StartCoroutine( StartSpawn());
    }

    private IEnumerator StartSpawn()
    {
        // ������ ���������� �ݺ�
        while (isPlaying)
        {
            // ������ ��ġ�� ����.
            int selectedIndex = Random.Range(0, points.Count); // 0 ~ 3
            //Debug.Log(points[selectedIndex]);

            var selectedTransform = points[selectedIndex];
            Instantiate(enemyGo, selectedTransform.position, selectedTransform.rotation);

            // ��� ����
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
