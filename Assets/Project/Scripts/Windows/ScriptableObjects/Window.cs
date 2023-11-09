using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Windows
{
    //[CreateAssetMenu(fileName = "New Window", menuName = "Window", order = 0)]
    public class Window : ScriptableObject
    {
        [SerializeField] private WindowNode node;
    }
}