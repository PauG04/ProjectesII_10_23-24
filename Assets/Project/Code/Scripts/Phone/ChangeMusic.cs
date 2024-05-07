using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeMusic : MonoBehaviour
{
    private int lastIndex = 7;
    public void ChangeSong(int index)
    {
        GameObject lastSong = transform.GetChild(lastIndex).gameObject;
        GameObject lastSoundImage = lastSong.transform.GetChild(1).gameObject;
        GameObject lastAnimationImage = lastSong.transform.GetChild(2).gameObject;
        lastSoundImage.SetActive(true);
        lastAnimationImage.SetActive(false);

        lastIndex = index;

        GameObject currentSong = transform.GetChild(index).gameObject;
        GameObject soundImage = currentSong.transform.GetChild(1).gameObject;
        GameObject animationImage = currentSong.transform.GetChild(2).gameObject;
        soundImage.SetActive(false);
        animationImage.SetActive(true);
        AudioManager.instance.PlayMusic(index);
    }
}
