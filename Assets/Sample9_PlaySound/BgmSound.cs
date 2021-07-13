using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgmSound : MonoBehaviour
{
    public float volume = 1;
    public AudioClip[] audioClip = new AudioClip[3];

    AudioSource audioSource;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.loop = true;
        StartCoroutine(PlayBgm());
    }

    float soundEndTime;
    
    private IEnumerator PlayBgm()
    {
        while(true)
        {
            for (int i = 0; i < audioClip.Length; i++)
            {
                audioSource.clip = audioClip[i];
                audioSource.Play();
                float soundtime = audioClip[i].length;
                soundEndTime = Time.time + soundtime;
                while (soundEndTime > Time.time)
                    yield return null;
            }
            yield return null; 
        }
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            // 다음 노래 재생하기.
            soundEndTime = 0;
        }
    }
}
