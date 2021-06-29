using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class PlayerNavMove : MonoBehaviour
{
    public NavMeshAgent agent;
    public LayerMask layer;
    void Start()
    {
        PanAndZoom panAndZoom = GetComponent<PanAndZoom>();

        panAndZoom.onTap += position => {
            Debug.Log("I've been tapped at " + position + "!, Input.mousePosition :" + Input.mousePosition);

            var ray = Camera.main.ScreenPointToRay(position);
            if (Physics.Raycast(ray, out RaycastHit hitData, 1000, layer))
            {
                agent.destination = hitData.point;
            }
        };
    }
}
