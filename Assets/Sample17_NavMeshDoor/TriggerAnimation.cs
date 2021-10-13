using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    public string enterAnimation;
    public string exitAnimation;
    private void OnTriggerEnter(Collider other)
    {
        //문 열리는 애니메이션 시작.
        print("Enter");
        GetComponent<Animator>().Play(enterAnimation);
    }
    private void OnTriggerExit(Collider other)
    {
        print("Exit");
        //문 닫히는 애니메이션 시작.
        GetComponent<Animator>().Play(exitAnimation);
    }
}
