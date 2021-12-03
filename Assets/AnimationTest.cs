using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTest : MonoBehaviour
{
    Animator animator;
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    [System.Serializable]
    public class AnimationInfo
    {
        public string clipName;
        public KeyCode keyCode;
        public bool isLoop;
        public int layer = 1;
    }
    public List<AnimationInfo> Clips;
    void Update()
    {
        foreach(var item in Clips)
        {
            if(Input.GetKeyDown(item.keyCode))
                PlayAnimation(item);
        }
    }

    private void PlayAnimation(AnimationInfo animationInfo)
    {
        string clipName = animationInfo.clipName;
        bool isLoop = animationInfo.isLoop;
        int layer = animationInfo.layer;

        if (isLoop)
            animator.Play(clipName);
        else
            animator.Play(clipName, layer, 0);
    }
}
