using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SFXSlider : MonoBehaviour
{
    public Slider sfxSlider;
    public AudioMixer mixer;

    private float sfxVolume;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 1f);
        sfxSlider.value = sfxVolume;
        mixer.SetFloat("SFXVolume", Mathf.Log10((sfxVolume) * 20));
        //SoundManager.instance.SetBGSoundVolume(sfxVolume);
    }
    private void Update()
    {
        mixer.SetFloat("SFXVolume", Mathf.Log10(sfxSlider.value) * 20);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }
}
