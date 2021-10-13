using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavMeshAreaDoor : MonoBehaviour
{
    public string doorLayerName = "Door";
    int doorLayer;
    private void Start()
    {
        doorLayer = 1 << NavMesh.GetAreaFromName(doorLayerName);
    }
    private void OnTriggerEnter(Collider other)
    {
        //문 열리는 애니메이션 시작.
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        agent.areaMask += doorLayer;
    }
    private void OnTriggerExit(Collider other)
    {
        //문 닫히는 애니메이션 시작.
        NavMeshAgent agent = other.GetComponent<NavMeshAgent>();
        agent.areaMask -= doorLayer;
    }
}
