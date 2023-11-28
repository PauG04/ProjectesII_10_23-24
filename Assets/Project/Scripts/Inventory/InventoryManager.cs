using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    private Dictionary<string, int> inventory;

    private void Awake()
    {
        inventory = new Dictionary<string, int>();
    }

    /// <summary>
    /// Scriptable object de items
    ///     strinnombre
    ///     renderer imagen
    ///     string descripcion
    ///     int precio
    /// </summary>
}
