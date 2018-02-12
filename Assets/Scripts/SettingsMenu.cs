using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour {
    public AudioMixer audioMixer;
    public Slider masterVolume;
    public GameObject bgmVolume;
    public GameObject sfxVolume;

    public float mstVol;
    public float bgmVol;
    public float sfxVol;

    public static SettingsMenu instance;
    void Awake()
    {
        
        //bgmVolume = GameObject.FindGameObjectWithTag("bgmSlider");
        //sfxVolume = GameObject.FindGameObjectWithTag("sfxSlider");

        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);


    }

    void Start()
    {
        masterVolume = GameObject.FindGameObjectWithTag("mstSlider").GetComponent<Slider>();

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
        mstVol = masterVolume.value;
        bgmVol = bgmVolume.GetComponent<Slider>().value;
        sfxVol = sfxVolume.GetComponent<Slider>().value;
    }
}
