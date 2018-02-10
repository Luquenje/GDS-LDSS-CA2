using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;

    public void SetVol(float volume)
    {
        audioMixer.SetFloat("volumeMaster", volume);
    }

    public void SetBGM (float volume)
    {
        audioMixer.SetFloat("volumeBGM", volume);
    }

    public void SetSFX(float volume)
    {
        audioMixer.SetFloat("volumeSFX", volume);
    }
}
