using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundManager : MonoBehaviour
{
    public bool useObjectPool = true;
    public AudioSource defaultAudioSource;
    static SoundManager m_instance;
    public int globalVolume = 1;
    static SoundManager Instance
    {
        get
        {
            if (m_instance == null)
            {
                GameObject go = new GameObject(nameof(SoundManager), typeof(SoundManager));
                m_instance = go.GetComponent<SoundManager>();
            }

            return m_instance;
        }
    }

    static bool applicationIsQuitting;

    void ApplicationIsQuitting()
    {
        applicationIsQuitting = true;
        //Debug.Log($"ApplicationIsQuitting:{applicationIsQuitting} {Time.realtimeSinceStartupAsDouble}");
    }
    private void Awake()
    {
        if (m_instance != null)
            return;

        m_instance = this;
        DontDestroyOnLoad(gameObject);

        Application.quitting += ApplicationIsQuitting;
        if (defaultAudioSource == null)
        {
            var newGo = new GameObject("AudioSource", typeof(AudioSource));
            defaultAudioSource = newGo.GetComponent<AudioSource>();
            defaultAudioSource.playOnAwake = false;
        }
    }

    public static void PlaySound(AudioClip audioClip, float volume, Vector3? position = null)
    {
        if (applicationIsQuitting)
            return;

        Instance._PlaySound(audioClip, volume, position);
    }

    private void _PlaySound(AudioClip audioClip, float volume, Vector3? position = null)
    {
        //position이 있으면 3D사운드(리스너와 거리에 따라 볼류크기 달라짐)로 재생.
        // 없으면 2D사운드(리스너와 거리가 멀어져도 소리크기 줄어들지 않음)로 재생(UI)
        // * 리스너는 기본으로 메인카메라에 달려 있음, 보통 캐릭터에 리스너 달아두는게 일반적임.

        //Debug.Log($"applicationIsQuitting:{applicationIsQuitting}, {audioClip.name} {Time.realtimeSinceStartupAsDouble}");

        // 에디터종료시는 사운드 재생하면 안된다.
        if (applicationIsQuitting)
        {
            return;
        }

        AudioSource audioSource = null;
        
        if(useObjectPool)
            audioSource = ObjectPool.Instantiate(defaultAudioSource);
        else
        {
            audioSource = Instantiate(defaultAudioSource);
        }

        if (position != null)
        {
            audioSource.transform.position = position.Value;
            audioSource.spatialBlend = 1; // 3D 리스너와 떨어진 거리에 따라 볼률 재생됨
        }
        else
        {
            audioSource.spatialBlend = 0; // 2D 거리에 상관없이 같은 볼륨 재생됨
        }

        audioSource.volume = globalVolume * volume;
        audioSource.clip = audioClip;
        audioSource.Play();

        if (useObjectPool)
            ObjectPool.Destroy(audioSource.gameObject, audioClip.length);
        else
            Destroy(audioSource.gameObject, audioClip.length);
    }
}
