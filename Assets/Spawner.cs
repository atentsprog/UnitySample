using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<Transform> points;
    public GameObject enemyGo;
    void Start()
    {
        int selectedIndex = Random.Range(0, points.Count); // 0 ~ 3
        Debug.Log(points[selectedIndex]);

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
