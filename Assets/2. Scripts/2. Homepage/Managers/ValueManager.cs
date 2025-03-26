using System;
using TMPro;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    // Geeft aan of een gebruiker een afspraak mag selecteren
    public bool canSelect = true;

    // ID van de momenteel geselecteerde afspraak (-1 betekent geen selectie)
    public int selectedPrefab = -1;

    // Houdt bij welk ID als laatste is gebruikt (standaard op 2 omdat er automatisch enkele afspraken zijn)
    public int lastID = -1;

    // Startdatum voor de afspraken
    public DateTime startDate;

    public int GetNewID()
    {
        // Verhoog de laatste ID en retourneer deze
        lastID += 1;
        return lastID;
    }

    // laat weten wat de naam is van de momentele geselcteerde prefab
    public string appointmentName;

    // laat weten wat de datum is van de momentele geselcteerde prefab
    public DateTime appointmentDate;
}
