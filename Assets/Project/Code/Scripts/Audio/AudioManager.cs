using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public AudioClip[] songs;
    private Sound songAudioSource;
    private int songCounter;

    public static AudioManager instance;

    [SerializeField]
    private AudioMixer mixer;

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

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }

        songAudioSource = Array.Find(sounds, sound => sound.soundName == "GameSong");
        songCounter = 0;
    }
    private void Start()
    {
        PlaySong("GameSong");
    }

    private void Update()
    {
        if (!songAudioSource.source.isPlaying)
        {
            songAudioSource.clip = songs[songCounter];
            PlaySong("GameSong");
            songCounter++;
            if (songCounter >= songs.Length)
            {
                songCounter = 0;
            }
        }
    }

    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("SFX")[0];
        s.source.Play();
    }

    public void PlaySong(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.outputAudioMixerGroup = mixer.FindMatchingGroups("Music")[0];
        s.source.Play();
    }


    public void SetPitch (string name, float pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.pitch = pitch;
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.soundName == sound);
        if (s == null)
            return;
        Debug.Log("stop sound");
        s.source.loop = false;
        s.source.Stop();
    }
}
