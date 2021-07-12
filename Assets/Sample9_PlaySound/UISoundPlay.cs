using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISoundPlay : MonoBehaviour
{
    public AudioClip AudioClip;
    public float volume = 1;

    private void Awake()
    {
        GetComponent<Button>().AddListener(this, PlaySound);
        //GetComponent<Button>().onClick.AddListener(_PlaySound);
    }

    private void PlaySound()
    {
        SoundManager.PlaySound(AudioClip, volume);
    }
}
