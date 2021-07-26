using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Sample11_Coroutine_Inheritance
{
    class Parent
    {

    }
    class Child1 : Parent
    {
        public int childInt;
        public void Child1Fn()
        {
            Debug.Log("Child1Fn");
        }
    }
    class Child2
    {
        public void Child2Fn()
        {
            Debug.Log("Child2Fn");
        }
    }

    public class Inheritance : MonoBehaviour
    {
        void Start()
        {
            //Child1 child1 = new Child1();
            //child1.Child1Fn();
        }
    }
}
