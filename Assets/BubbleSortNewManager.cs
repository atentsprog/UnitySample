using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleSortNewManager : MonoBehaviour
{
    public BubbleSortNewItem cube;
    public Vector3 offset = new Vector3(0, 1.5f, 0);

    public List<int> intArray;
    public List<BubbleSortNewItem> cubes;
    IEnumerator Start()
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
                if (downNumber > upNumber)
                {
                    //intArray에 있는 숫자 바꾸기.
                    Swap(x, intArray);

                    //박스의 위치 바꾸기
                    var downCube = cubes[x];
                    var upCube = cubes[x + 1];
                    var downPosition = downCube.transform.position;
                    var upPosition = upCube.transform.position;

                    //cubes박스정보도 스왑하자.
                    cubes[x] = upCube;
                    cubes[x + 1] = downCube;

                    downCube.transform.DOMove(upPosition, 0.5f).SetEase(Ease.InBounce);
                    upCube.transform.DOMove(downPosition, 0.5f).SetEase(Ease.InBounce);
                    yield return new WaitForSeconds(0.5f);
                }
            }
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
}
