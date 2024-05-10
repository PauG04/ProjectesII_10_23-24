using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeMusic : MonoBehaviour
{
    private int lastIndex = 7;

    private void Start()
    {

    }

    public void ChangeSong(int index)
    {
        GameObject lastSong = transform.GetChild(lastIndex).gameObject;
        GameObject lastAnimationImage = lastSong.transform.GetChild(2).gameObject;
        GameObject lastSongImage = lastSong.transform.GetChild(1).gameObject;
        lastSongImage.GetComponent<Image>().color = new Color(1f, 1f, 1f, 1f);

        lastAnimationImage.SetActive(false);

        lastIndex = index;

        GameObject currentSong = transform.GetChild(index).gameObject;
        GameObject animationImage = currentSong.transform.GetChild(2).gameObject;
        GameObject songImage = currentSong.transform.GetChild(1).gameObject;
        songImage.GetComponent<Image>().color = new Color(0.5f, 0.5f, 0.5f, 1f);
        animationImage.SetActive(true);
        AudioManager.instance.PlayMusic(index);
    }
}
