using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortNewManager : MonoBehaviour
{
    public BubbleSortNewItem cube;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    public List<int> intArray;
    public List<BubbleSortNewItem> cubes;
    private Coroutine sortCoHandle;

    void Start()
    {
        StartSort();
    }

    IEnumerator SortCo()
    { 
        //intArray <- 이용해서 박스를 만들자.
        //item + 위치값 더한곳에 생성하자.
        cube.gameObject.SetActive(false);
        for (int i = 0; i < intArray.Count; i++)
        {
            var pos = cube.transform.position + offset * i;
            BubbleSortNewItem newItem = Instantiate(cube, pos, cube.transform.rotation);
            newItem.SetNumber(intArray[i]);
            newItem.gameObject.SetActive(true);
            cubes.Add(newItem);
        }

        yield return new WaitForSeconds(0.5f);

        for (int turn = 0; turn < intArray.Count; turn++)
        {
            int fixCount = turn;
            int checkLength = intArray.Count - fixCount;
            checkLength = checkLength - 1;// 오른쪽 왼쪽을 한쌍으로 검사하므로 최대길이 -1까지만 검사하면 된다

            for (int x = 0; x < checkLength; x++)
            {
                int downNumber = intArray[x]; 
                int upNumber = intArray[x + 1];

                // todo: 지금 체크 하고 있는거 노란색으로 표시.
                var downCube = cubes[x];
                var upCube = cubes[x + 1];
                yield return StartCoroutine(
                    ChangeColorCo(upCube, downCube, Color.yellow));
                
                if (downNumber > upNumber)
                {
                    // 빨간색 지정.
                    yield return StartCoroutine(
                        ChangeColorCo(upCube, downCube, Color.red)); 
                    //upCube.ChangeColor(new Color(1, 0, 0)); // Color.red
                    //downCube.ChangeColor(new Color32(255, 0, 0, 0));

                    //intArray에 있는 숫자 바꾸기.
                    Swap(x, intArray);

                    //박스의 위치 바꾸기
                    var downPosition = downCube.transform.position;
                    var upPosition = upCube.transform.position;

                    //cubes박스정보도 스왑하자.
                    cubes[x] = upCube;
                    cubes[x + 1] = downCube;

                    downCube.transform.DOMove(upPosition, 0.5f).SetEase(Ease.InBounce);
                    upCube.transform.DOMove(downPosition, 0.5f).SetEase(Ease.InBounce);
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    yield return StartCoroutine(
                        ChangeColorCo(upCube, downCube, Color.green));
                }

                yield return StartCoroutine
                    (ChangeColorCo(upCube, downCube, Color.white));
            }

            // 고정된 블락을 회색으로 표시
            cubes[intArray.Count - 1 - turn].ChangeColor(Color.gray);
        }

        void Swap(int index, List<int> intArray)
        {
            int leftIndex = index;
            int rightIndex = index + 1;

            int temp = intArray[leftIndex];
            intArray[leftIndex] = intArray[rightIndex];
            intArray[rightIndex] = temp;
        }
    }

    private IEnumerator ChangeColorCo(BubbleSortNewItem upCube, BubbleSortNewItem downCube, Color color)
    {
        upCube.ChangeColor(color);
        downCube.ChangeColor(color);
        yield return new WaitForSeconds(0.5f);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            StartSort();
        }
    }

    private void StartSort()
    {
        // 기존에 있던거를 뭐춰야한다. <- 구현안하고
        if(sortCoHandle != null)
            StopCoroutine(sortCoHandle);

        // 기존의 cube삭제
        cubes.ForEach(x => Destroy(x.gameObject));
        cubes.Clear();

        // 다시 소트 시작.
        sortCoHandle = StartCoroutine(SortCo());
    }
}
