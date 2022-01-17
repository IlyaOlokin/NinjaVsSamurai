using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundButton : MonoBehaviour
{
    [SerializeField]
    private GameObject soundIndicator;

    private void Start()
    {
        ApplySoundIndicator(FindObjectOfType<AudioManager>().soundEnabled);
    }

    public void SwitchSound()
    {
        var am = FindObjectOfType<AudioManager>();
        FindObjectOfType<AudioManager>().Play("ButtonClick");

        if (am.soundEnabled) am.SoundOff();
        else am.SoundOn();
        
        ApplySoundIndicator(am.soundEnabled);
    }

    private void ApplySoundIndicator(bool soundEnabled)
    {
        soundIndicator.SetActive(soundEnabled);
    }
}
