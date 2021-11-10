using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample21_CollideDrag
{
    public class KeepLocalPosition : MonoBehaviour
    {
        Vector3 originalLocalPos;
        public float lerpValue = 0.05f;

        void Start()
        {
            originalLocalPos = transform.localPosition;
        }

        void Update()
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalLocalPos, lerpValue);
        }
    }
}
