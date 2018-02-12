using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public GameObject masterVolume;
    public GameObject bgmVolume;
    public GameObject sfxVolume;
    public GameObject optionsMenu;

    public float mstVol;
    public float bgmVol;
    public float sfxVol;

    public bool options;

    public static SettingsMenu instance;
    void Awake()
    {
        
        masterVolume = GameObject.FindGameObjectWithTag("mstSlider");
        bgmVolume = GameObject.FindGameObjectWithTag("bgmSlider");
        sfxVolume = GameObject.FindGameObjectWithTag("sfxSlider");
        optionsMenu.SetActive(false);

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


    }

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

    private void Update()
    {
        audioMixer.GetFloat("volumeMaster", out mstVol);
        audioMixer.GetFloat("volumeBGM", out bgmVol);
        audioMixer.GetFloat("volumeSFX", out sfxVol);
        mstVol = masterVolume.GetComponent<Slider>().value;
        bgmVol = bgmVolume.GetComponent<Slider>().value;
        sfxVol = sfxVolume.GetComponent<Slider>().value;
    }
}
