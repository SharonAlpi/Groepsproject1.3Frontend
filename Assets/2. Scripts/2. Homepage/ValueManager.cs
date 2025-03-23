using System;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    /// <summary>
    /// Controls which object is selected and if it can be selected.
    /// When an object is clicked, selection is locked. 
    /// Only when editing is finished can objects be selected again.
    /// </summary>
    public bool canSelect = true;
    public int selectedPrefab = -1; // -1 means no selection
    public int lastID = 2; //since we place some automaticly

    internal int GetNewID()
    {
        lastID = +1;
        return lastID;
    }
}
