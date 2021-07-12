using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlay : MonoBehaviour
{
    public enum TimingType
    {
        Awake,
        Start,
        Enable,
        Disable,
        Destroy
    }

    public TimingType playTiming = TimingType.Start;
    public AudioClip AudioClip;
    public float volume = 1;
    public bool uISound = false;

    private void Awake()
    {
        _PlaySound(TimingType.Awake);
    }
    void Start()
    {
        _PlaySound(TimingType.Start);
    }
    private void OnEnable()
    {
        _PlaySound(TimingType.Enable);
    }
    private void OnDisable()
    {
        _PlaySound(TimingType.Disable);
    }
    private void OnDestroy()
    {
        _PlaySound(TimingType.Destroy);
    }

    private void _PlaySound(TimingType currentTime)
    {
        if (currentTime != playTiming)
            return;

        Vector3? pos = null;
        if (uISound == false)
            pos = transform.position;
        SoundManager.PlaySound(AudioClip, volume, pos);
    }
}
