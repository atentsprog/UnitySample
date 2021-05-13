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
        // ���� ���ҽ� �ʱ�ȭ.
        // ���� ���� ������Ʈ �ı�
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

        yield return StartCoroutine(LogAndWaitCo($"{log}�� ���ؼ� ���� �����մϴ�."));

        for (int turn = 0; turn < items.Count; turn++)
        {
            int fixCount = turn;
            int checkLength = items.Count - fixCount;
            checkLength = checkLength - 1;// ������ ������ �ѽ����� �˻��ϹǷ� �ִ���� -1������ �˻��ϸ� �ȴ�

            yield return StartCoroutine(LogAndWaitCo($"�� {turn + 1} ����"));

            for (int x = 0; x < checkLength; x++)
            {
                int leftIndex = x;
                int rightIndex = x + 1;

                var leftBox = items[leftIndex];
                var rightBox = items[rightIndex];

                //�˻��ϴ� �ڽ� �� ����
                leftBox.SetColor(Color.yellow);
                rightBox.SetColor(Color.red);

                int leftNumber = leftBox.Number;
                int rightNumber = rightBox.Number;

                yield return StartCoroutine(LogAndWaitCo($"{leftIndex}�� {rightIndex}��"));

                if (leftNumber > rightNumber)
                {
                    infoText = $"{leftIndex}�� {rightIndex} ��ġ ��ȯ";

                    // ���� ��ġ �̵�.
                    yield return Swap(x, leftBox, rightBox);
                }

                leftBox.SetColor(Color.white);
                rightBox.SetColor(Color.white);

                yield return new WaitForSeconds(simulationSpeed);
            }

            yield return StartCoroutine(LogAndWaitCo($"�� {turn + 1} ����"));
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
        yield return null; // todo: �ε巴�� �ִϸ��̼� ��Ű��.


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

        if (GUI.Button(new Rect(20, 40, 80, 20), "����"))
        {
            Debug.Log("����");
            SortStart();
        }

        //if (GUI.Button(new Rect(20, 70, 80, 20), "�ܰ躰 ����."))
        //{
        //    Debug.Log("�ܰ躰 ����");
        //}
    }
}
