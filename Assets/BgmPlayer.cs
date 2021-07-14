using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BgmPlayer : MonoBehaviour
{
    [System.Serializable]
    public class BgmInfo
    {
        public AudioClip audioClip;
        public float volume;

        public float fadeInTime = 3;
        public float fadeInBeginVolume = 0.3f;

        public float fadeOutTime = 2;
    }

    public BgmInfo[] Bgms;
    int playBgmIndex;


    AudioSource[] audioSources;
    int audioSourcesIndex;
    void Start()
    {
        DontDestroyOnLoad(gameObject);

        audioSources = GetComponents<AudioSource>();
        foreach (var item in audioSources)
        {
            item.loop = true;
            item.spatialBlend = 0;
        }

        PlayBgmSound(Bgms[playBgmIndex]);
    }

    private void PlayBgmSound(BgmInfo bgm)
    {
        AudioSource audioSource = audioSources[audioSourcesIndex];
        audioSource.clip = bgm.audioClip;
        audioSource.volume = bgm.volume;

        if (bgm.fadeInTime > 0)
        {
            audioSource.volume = bgm.fadeInBeginVolume;

            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, bgm.volume, bgm.fadeInTime);

            //audioSource.DOFade(bgm.volume, bgm.fadeInTime); // 위와 같은 결과.
        }
        else
        {
            audioSource.volume = bgm.volume;
        }

        audioSource.Play();
    }

    public void PlayNextBgm()
    {
        StopBgmSound(Bgms[playBgmIndex]);

        playBgmIndex++;
        if (playBgmIndex >= Bgms.Length)
            playBgmIndex = 0;


        audioSourcesIndex++;
        if (audioSourcesIndex >= audioSources.Length)
            audioSourcesIndex = 0;


        PlayBgmSound(Bgms[playBgmIndex]);
    }

    private void StopBgmSound(BgmInfo bgmInfo)
    {
        var audioSource = audioSources[audioSourcesIndex];
        if (bgmInfo.fadeOutTime > 0)
        {
            DOTween.To(() => audioSource.volume, x => audioSource.volume = x, 0, bgmInfo.fadeOutTime)
                .OnComplete(()=> {
                    audioSource.Stop();
                    audioSource.clip = null;
                });
        }
        else
        { 
            audioSource.Stop();
            audioSource.clip = null;
        }
    }
}
