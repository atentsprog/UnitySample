using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceLoadTest : MonoBehaviour
{
    public string readObjectName = "Cube";
    public void LoadResourceFn()
    {
        // 메모리상으로 로드, 게임오브젝트, 이미지, 사운드,.. 모든것 로드 가능.
        // 파일 이름 같을 경우 타입지정해서 구분 가능
        Object memoryObj = Resources.Load(readObjectName);
        //GameObject memoryObj = (GameObject)Resources.Load(readObjectName, typeof(GameObject));

        GameObject memoryGo = (GameObject)memoryObj;
        GameObject sceneGo = Instantiate(memoryGo);
        sceneGo.name = "리소스에서 불러온" + memoryGo.name;
    }
}
