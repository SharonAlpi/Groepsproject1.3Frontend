using System;
using TMPro;
using UnityEngine;

public class ValueManager : MonoBehaviour
{
    // Geeft aan of een gebruiker een afspraak mag selecteren
    public bool canSelect = true;

    // Geeft aan of een gebruiker een afspraak mag selecteren'=
    public int currentPerson = 0;

    // ID van de momenteel geselecteerde afspraak (-1 betekent geen selectie)
    public Guid selectedPrefab;

    public Guid UsersID;

    // Houdt bij welk ID als laatste is gebruikt (standaard op 2 omdat er automatisch enkele afspraken zijn)
    public int lastID = 1;

    // Startdatum voor de afspraken
    public DateTime startDate = DateTime.Now.Date;

    // momentuele datum voor de afspraken
    public DateTime currentDate = DateTime.Now.Date.AddDays(5);

    // of er momenteel een appointment is op de dag
    public bool hasAppoimnet;

    // momentuele id voor de afspraken op dit moment
    public Guid currentAppoimnetId;

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
