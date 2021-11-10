using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample21_CollideDrag
{
    public class RemoveRigidbodyVelocity : MonoBehaviour
    {
        new Rigidbody rigidbody;
        void Start()
        {
            rigidbody = GetComponent<Rigidbody>();

        }
        void Update()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
    }
}