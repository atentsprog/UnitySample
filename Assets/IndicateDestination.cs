using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace Sample17_NavMesh
{
    public class IndicateDestination : MonoBehaviour
    {

        public NavMeshAgent agent;
        public LineRenderer lineRenderer;
        public Transform desTr;
        void Start()
        {
            agent = GetComponent<NavMeshAgent>();
            lineRenderer = GetComponent<LineRenderer>();
        }
        // Update is called once per frame
        void Update()
        {
            NavMeshPath path = new NavMeshPath();
            agent.CalculatePath(desTr.position, path);
            lineRenderer.positionCount = path.corners.Length;
            List<Vector3> corners = new List<Vector3>(path.corners);
            //corners.RemoveAt(corners.Count - 1);
            lineRenderer.SetPositions(corners.ToArray());
        }
    }
}