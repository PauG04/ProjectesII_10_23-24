using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMusic : MonoBehaviour
{
    public void ChangeSong(int index)
    {
        AudioManager.instance.PlayMusic(index);
    }
}
