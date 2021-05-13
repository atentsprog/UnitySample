using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class BubbleISortManager : MonoBehaviour
{
    public BubbleSortItem item;
    public float widthOffset = 110;
    public float simulationSpeed = 0.2f;
    public List<int> intArray = new List<int>{ 1, 5, -1, 4, 0 };

    private void Start()
    {
        SortStart();
    }

    private void SortStart()
    {
        // 기존 리소스 초기화.
        // 기존 게임 오브젝트 파괴
        items.ForEach(x => Destroy(x.gameObject));
        items.Clear();

        StartCoroutine(SortStartCo());
    }

    List<BubbleSortItem> items = new List<BubbleSortItem>();
    IEnumerator SortStartCo()
    {
        item.gameObject.SetActive(false);


        for (int i = 0; i < intArray.Count; i++)
        {
            int number = intArray[i];
            Vector3 pos = transform.position;
            pos.x += widthOffset * i;
            var newItem = Instantiate(item, pos, Quaternion.identity);
            newItem.gameObject.SetActive(true);
            newItem.Number = number;
            items.Add(newItem);
        }

        string log = intArray.Select(x => x.ToString())
            .Aggregate((x1, x2 )=> { return $"{x1}, {x2}"; });

        yield return StartCoroutine(LogAndWaitCo($"{log}에 대해서 정렬 시작합니다."));

        for (int turn = 0; turn < items.Count; turn++)
        {
            int fixCount = turn;
            int checkLength = items.Count - fixCount;
            checkLength = checkLength - 1;// 오른쪽 왼쪽을 한쌍으로 검사하므로 최대길이 -1까지만 검사하면 된다

            yield return StartCoroutine(LogAndWaitCo($"턴 {turn + 1} 시작"));

            for (int x = 0; x < checkLength; x++)
            {
                int leftIndex = x;
                int rightIndex = x + 1;

                var leftBox = items[leftIndex];
                var rightBox = items[rightIndex];

                //검사하는 박스 색 변경
                leftBox.SetColor(Color.yellow);
                rightBox.SetColor(Color.red);

                int leftNumber = leftBox.Number;
                int rightNumber = rightBox.Number;

                yield return StartCoroutine(LogAndWaitCo($"{leftIndex}와 {rightIndex}비교"));

                if (leftNumber > rightNumber)
                {
                    infoText = $"{leftIndex}와 {rightIndex} 위치 교환";

                    // 실제 위치 이동.
                    yield return Swap(x, leftBox, rightBox);
                }

                leftBox.SetColor(Color.white);
                rightBox.SetColor(Color.white);

                yield return new WaitForSeconds(simulationSpeed);
            }

            yield return StartCoroutine(LogAndWaitCo($"턴 {turn + 1} 종료"));
        }
    }

    private IEnumerator LogAndWaitCo(string log)
    {
        infoText = log;
        yield return new WaitForSeconds(simulationSpeed);
    }

    IEnumerator Swap(int index, BubbleSortItem leftBox, BubbleSortItem rightBox)
    {
        int leftIndex = index;
        int rightIndex = index + 1;

        var tempPos = leftBox.transform.position;
        leftBox.transform.position = rightBox.transform.position;
        rightBox.transform.position = tempPos;
        yield return null; // todo: 부드럽게 애니메이션 시키자.


        var tempBox = items[leftIndex];
        items[leftIndex] = items[rightIndex];
        items[rightIndex] = tempBox;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Time.timeScale = Time.timeScale == 0 ? 1 : 0;
    }

    string infoText;
    private void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, Screen.width - 20, 90), infoText);

        if (GUI.Button(new Rect(20, 40, 80, 20), "시작"))
        {
            Debug.Log("시작");
            SortStart();
        }

        //if (GUI.Button(new Rect(20, 70, 80, 20), "단계별 진행."))
        //{
        //    Debug.Log("단계별 진행");
        //}
    }
}
