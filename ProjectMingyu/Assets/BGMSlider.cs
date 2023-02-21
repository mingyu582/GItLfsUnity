using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{
    public Slider bgmSlider;
    public AudioMixer mixer;

    private float bgVolume;

    private void Start()
    {
        //PlayerPrefs.DeleteAll();
        bgVolume = PlayerPrefs.GetFloat("BGSoundVolume", 1f);
        bgmSlider.value = bgVolume;
        mixer.SetFloat("BGSoundVolume", Mathf.Log10((bgVolume) * 20));
        SoundManager.instance.SetBGSoundVolume(bgVolume);
    }
    private void Update()
    {
        mixer.SetFloat("BGSoundVolume", Mathf.Log10(bgmSlider.value) * 20);
        PlayerPrefs.SetFloat("BGSoundVolume", bgmSlider.value);
    }
}
