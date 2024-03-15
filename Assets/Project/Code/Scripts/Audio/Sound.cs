using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string soundName;
    public AudioClip clip;

    [Range(0, 1)] public float volume;

    [Range(-3, 3)] public float minPitch;
    [Range(-3, 3)] public float maxPitch;
    public bool loop;
}
