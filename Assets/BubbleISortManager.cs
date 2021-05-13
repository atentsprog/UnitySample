using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using DG.Tweening;

public class BubbleISortManager : MonoBehaviour
{
    public BubbleSortItem item;
    public float widthOffset = 110;
    public float simulationSpeed = 0.2f;
    public List<int> intList = new List<int>{ 1, 5, -1, 4, 0 };

    private void Start()
    {
        SortStart();
    }

    Coroutine sortStartCoHandle;
    private void SortStart()
    {
        if (sortStartCoHandle != null)
            StopCoroutine(sortStartCoHandle);

        sortStartCoHandle = StartCoroutine(SortStartCo());
    }

    List<BubbleSortItem> items = new List<BubbleSortItem>();
    IEnumerator SortStartCo()
    {
        item.gameObject.SetActive(false);


        // 기존 게임 오브젝트 파괴
        string log = MakeNumberBoxes();

        yield return StartCoroutine(LogAndWaitCo($"{log}에 대해서 정렬 시작합니다."));

        for (int turn = 0; turn < items.Count; turn++)
        {
            int fixCount = turn;
            int checkLength = items.Count - fixCount;
            int lastBoxIndex = checkLength - 1;// 오른쪽 왼쪽을 한쌍으로 검사하므로 최대길이 -1까지만 검사하면 된다

            yield return StartCoroutine(LogAndWaitCo($"턴 {turn + 1} 시작"));

            for (int x = 0; x < lastBoxIndex; x++)
            {
                int leftIndex = x;
                int rightIndex = x + 1;

                var leftBox = items[leftIndex];
                var rightBox = items[rightIndex];

                // 검사하는 박스 색 변경 -> 노란색으로 변경
                // 위치 교화 해야하는건 빨간색으로 표시.
                // 위치 교환 안해도 되는건 녹색으로 표시.
                // 위치 이동 완료후 기본색(하얀색)으로 표시.

                List<BubbleSortItem> leftAndRightbox = new List<BubbleSortItem> { leftBox, rightBox };
                leftAndRightbox.ForEach(x => x.SetColor(Color.yellow));

                int leftNumber = leftBox.Number;
                int rightNumber = rightBox.Number;

                yield return StartCoroutine(LogAndWaitCo($"{leftIndex}와 {rightIndex}비교"));

                if (leftNumber > rightNumber)
                {
                    leftAndRightbox.ForEach(x => x.SetColor(Color.red));

                    infoText = $"{leftIndex}와 {rightIndex} 위치 교환";

                    // 실제 위치 이동.
                    yield return Swap(x, leftBox, rightBox);
                }
                else
                {
                    leftAndRightbox.ForEach(x => x.SetColor(Color.green));
                }

                yield return new WaitForSeconds(simulationSpeed);
                leftAndRightbox.ForEach(x => x.SetColor(Color.white));

                yield return new WaitForSeconds(simulationSpeed);
            }

            var fixBox = items[lastBoxIndex];
            fixBox.SetColor(Color.gray);

            yield return StartCoroutine(LogAndWaitCo($"턴 {turn + 1} 종료"));
        }

        items.ForEach(x => x.transform.DOPunchScale(x.transform.localScale, 0.5f));
    }

    private string MakeNumberBoxes()
    {
        items.ForEach(x => Destroy(x.gameObject));
        items.Clear();

        for (int i = 0; i < intList.Count; i++)
        {
            int number = intList[i];
            Vector3 pos = transform.position;
            pos.x += widthOffset * i;
            var newItem = Instantiate(item, pos, Quaternion.identity);
            newItem.gameObject.SetActive(true);
            newItem.Number = number;
            items.Add(newItem);
        }

        string log = intList.Select(x => x.ToString())
            .Aggregate((x1, x2) => { return $"{x1}, {x2}"; });
        return log;
    }

    private IEnumerator LogAndWaitCo(string log)
    {
        infoText = log;
        yield return new WaitForSeconds(simulationSpeed);
    }
    public float tweenTime = 1f;
    public Ease easeType = Ease.OutExpo;
    IEnumerator Swap(int index, BubbleSortItem leftBox, BubbleSortItem rightBox)
    {
        int leftIndex = index;
        int rightIndex = index + 1;

        var leftPos = leftBox.transform.position;
        var rightPos = rightBox.transform.position;
        leftBox.transform.DOMove(rightPos, tweenTime).SetEase(easeType);
        yield return rightBox.transform.DOMove(leftPos, tweenTime).SetEase(easeType).WaitForCompletion();
       
        var tempBox = items[leftIndex];
        items[leftIndex] = items[rightIndex];
        items[rightIndex] = tempBox;
    }


    string infoText;
    private void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, Screen.width - 20, 90), infoText);

        if (GUI.Button(new Rect(20, 40, 200, 20), "시작"))
        {
            Debug.Log("시작");
            SortStart();
        }

        if (GUI.Button(new Rect(20, 70, 200, 20), "랜덤 번호 지정"))
        {
            //Debug.Log("랜덤 번호 지정");
            SetRandomNumber();
        }
    }

    private void SetRandomNumber()
    {
        int min = 1;
        int max = intList.Count + 1;
        for (int i = 0; i < intList.Count; i++)
        {
            intList[i] = UnityEngine.Random.Range(min, max);
        }

        // 텍스트 정보 표시.
        // 박스 새로 만들기.
        infoText = MakeNumberBoxes() + "로 숫자 지정했습니다";
    }
}
