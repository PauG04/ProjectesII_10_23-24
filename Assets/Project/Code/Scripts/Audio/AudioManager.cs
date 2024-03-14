using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sfxSounds;
    public Sound[] musicSounds;
    [Space(10)]
    public AudioSource[] sfxSources;
    public AudioSource musicSource;

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

        songCounter = 0;
    }
    private void Start()
    {
        PlaySong(musicSounds[songCounter].soundName);
    }

    private void Update()
    {
        if (!musicSource.isPlaying)
        {
            songCounter++;
            if (songCounter == musicSounds.Length)
                songCounter = 0;
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

    public void StopPlayingSFX(string name)
    {
        Sound s = Array.Find(sfxSounds, sound => sound.soundName == name);

        if (s == null)
        {
            Debug.Log("Sound does not exist");
            return;
        }

        foreach (AudioSource audioSource in sfxSources)
        {
            if (audioSource.clip.name == name)
            {
                audioSource.Stop();
                return;
            }
        }
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
        musicSource.loop = false;
        musicSource.Play();
    }

    public bool IsPlayingSFX(string name) 
    {
        foreach (AudioSource audioSource in sfxSources)
        {
            if (audioSource.clip.name == name)
            {
                return audioSource.isPlaying;
            }
        }

        Debug.Log("Sound does not exist");
        return false;
    }
}
