﻿using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sfxSounds;
    public Sound[] musicSounds;
    [Space(10)]
    public AudioSource[] sfxSources;
    public AudioSource musicSource;
    public AudioSource liquidSource;

    public static AudioManager instance;

    private int songCounter;

    void Awake()
    {
	    if (instance == null)
	    {
		    instance = this;
	    }
        else
        {
	        Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);

        songCounter = 7;
    }
    private void Start()
    {
        PlaySong(musicSounds[songCounter].soundName);
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            PlaySong(musicSounds[songCounter].soundName);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (AudioSource audioSource in sfxSources)
        {
            if (!audioSource.isPlaying)
                return audioSource;
        }
        return null;
    }

    public void PlaySFX(string name, float customPitch = 5.0f)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound does not exist");
            return;
        }

        AudioSource audioSource = GetAvailableAudioSource();

        if (audioSource == null)
        {
            Debug.Log("No audio sources available");
            return;
        }

        if (customPitch == 5.0f)
            audioSource.pitch = UnityEngine.Random.Range(s.minPitch, s.maxPitch);
        else
            audioSource.pitch = customPitch;

        audioSource.volume = s.volume;
        audioSource.loop = s.loop;
        audioSource.PlayOneShot(s.clip);
    }

    public void PlayLiquidSFX()
    {
        Sound s = Array.Find(sfxSounds, sound => sound.soundName == "DropLiquid");
        
        if (!liquidSource.isPlaying)
        {
            liquidSource.clip = s.clip;
            liquidSource.pitch = UnityEngine.Random.Range(s.minPitch, s.maxPitch);
            liquidSource.volume = s.volume;
            liquidSource.loop = s.loop;
            liquidSource.PlayOneShot(s.clip);
        }
    }
    public void StopPlayingLiquidSFX()
    {
        liquidSource.Stop();
        liquidSource.loop = false;
    }

    public void PlaySong(string name)
    {
        Sound s = Array.Find(musicSounds, sound => sound.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound does not exist");
            return;
        }

        musicSource.volume = s.volume;
        musicSource.pitch = s.minPitch;
        musicSource.clip = s.clip;
        musicSource.loop = true;
        musicSource.Play();
    }

    public void PlayMusic(int index)
    {
        PlaySong(musicSounds[index].soundName);
    }
}
