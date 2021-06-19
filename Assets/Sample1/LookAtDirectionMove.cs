using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;

public class LookAtDirectionMove : MonoBehaviour
{
    public float speed = 5f;

    public Animator animator;

    private void Update()
    {
        // WASD, W위로, A왼쪽,S아래, D오른쪽
        Vector3 move = Vector3.zero;
        
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) move.z = 1;
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) move.z = -1;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) move.x = -1;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) move.x = 1;

        if (move != Vector3.zero)
        {
            move.Normalize();
            Vector3 initPosition = transform.position;
            Vector3 newPos = initPosition;
            newPos.x = initPosition.x + move.x * speed * Time.deltaTime;
            newPos.z = initPosition.z + move.z * speed * Time.deltaTime;
            transform.position = newPos;
            transform.LookAt(initPosition + move);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            State = StateType.Attack;
            StartCoroutine(OnFireArrowCo());
        }

        if (State != StateType.Attack)
        {
            if (move != Vector3.zero)
            {
                State = StateType.Run;
            }
            else
            {
                State = StateType.Idle;
            }
        }
    }
    private IEnumerator OnFireArrowCo()
    {
        float animationDuration = ClipLengthMap[clipInfoMap[StateType.Run].stateName];
        float afterDelay = animationDuration - fireDelay;
        // 잠시 쉬었다가
        yield return new WaitForSeconds(fireDelay);

        //애로우를 발사.
        Instantiate(arrowGo, arrowSpawnPosition.position, transform.rotation);

        yield return new WaitForSeconds(afterDelay);
        State = StateType.None;
    }

    public GameObject arrowGo;
    public Transform arrowSpawnPosition;
    public float fireDelay = 0.2f;

    [System.Serializable]
    public class AnimationClipInfo
    {
        public StateType state;
        public string stateName;
        public float transitionDuration = 0.0f; // 0 ~ 1까지
    }

    public List<AnimationClipInfo> clipInfos;
    Dictionary<StateType, AnimationClipInfo> clipInfoMap;
    Dictionary<StateType, AnimationClipInfo> ClipInfoMap
    {
        get
        {
            if (clipInfoMap == null)
            {
                clipInfoMap = clipInfos.ToDictionary(x => x.state);
            }
            return clipInfoMap;
        }
    }

    Dictionary<string, float> clipLengthMap = null;
    Dictionary<string, float> ClipLengthMap
    {
        get { 
            if(clipLengthMap == null)
            {
                clipLengthMap = new Dictionary<string, float>();
                RuntimeAnimatorController rAController = animator.runtimeAnimatorController;
                foreach (AnimationClip item in rAController.animationClips)
                {
                    clipLengthMap.Add(item.name, item.length);
                }
            }
            return clipLengthMap;
        }
    }
    public StateType state = StateType.None;
    public enum StateType
    {
        None,
        Attack,
        Idle,
        Run,
    }
    public StateType State
    {
        set {
            if (state == value)
                return;
            
            state = value;

            if(ClipInfoMap.ContainsKey(state))
            {
                var animationClip = ClipInfoMap[state];
                animator.CrossFade(animationClip.stateName, animationClip.transitionDuration);
            }
        }
        get { return state; }
    }
}
