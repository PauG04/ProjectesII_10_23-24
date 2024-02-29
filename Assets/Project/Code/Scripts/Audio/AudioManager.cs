using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

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
    }

    public void Play(string name, string mixerName, bool loop = false)
    {
        Sound s = Array.Find(sounds, sound => sound.soundName == name);
        s.source.outputAudioMixerGroup = mixer.FindMatchingGroups(mixerName)[0];
        s.source.loop = loop;
        s.source.Play();
    }

    public void StopPlaying(string sound)
    {
        Sound s = Array.Find(sounds, item => item.soundName == sound);
        if (s != null)
            return;
        s.source.Stop();
    }
}
