using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New Windows")]
public class WindowDataObject : ScriptableObject
{
    public SerializableWindows values;
}

[System.Serializable]
public class SerializableWindows
{
    [Header("General Configuration")]
    public string name;
    public string description;
    public Sprite image;

    [Header("Windows Size")]
    public int width;
    public int height;

    [Header("Inside Windows")]
    public GameObject prefabInside;
}