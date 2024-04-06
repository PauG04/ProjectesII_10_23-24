using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioOptions : MonoBehaviour
{
    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Audio Sliders")]
    [SerializeField] private Slider generalSound;
    [SerializeField] private Slider musicSound;
    [SerializeField] private Slider SFXSound;

    private void Awake()
    {
        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }

    public void SetMasterVolume()
    {
        float volume = generalSound.value;
        audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void SetMusicVolume()
    {
        float volume = musicSound.value;
        audioMixer.SetFloat("Music", Mathf.Log10(volume)*20);
        //PlayerPrefs.SetFloat("musicVolume", volume);

    }
    public void SetSFXVolume()
    {
        float volume = SFXSound.value;
        audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        //PlayerPrefs.SetFloat("SFXVolume", volume);
    }
    public void LoadVolume()
    {
        //generalSound.value = PlayerPrefs.GetFloat("masterVolume", 1);
        //musicSound.value = PlayerPrefs.GetFloat("musicVolume", 1);
        //SFXSound.value = PlayerPrefs.GetFloat("SFXVolume", 1);

        SetMasterVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
}
