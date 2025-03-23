using UnityEngine;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;

public class AppointmentInstantiator : MonoBehaviour
{
    // Lijst van prefab-objecten voor afspraken
    public List<GameObject> appointmentPrefabs;

    // Transform voor de plek waar afspraken worden geplaatst in de hiërarchie
    public Transform parentTransform;

    // Startdatum voor de afspraken
    private DateTime startDate;

    // Verwijzing naar de AppointmentSorter voor het sorteren van afspraken
    public ValueManager valueManager;
    public AppointmentSorter sorter;

    void Start()
    {
        // Zet startdatum en begin-ID
        startDate = DateTime.Now;

        // Maak afspraken aan
        CreateAppointments();
    }

    // Maakt afspraken aan op basis van de prefab-lijst
    void CreateAppointments()
    {
        foreach (var prefab in appointmentPrefabs)
        {
            // Maak nieuwe afspraakinstantie
            GameObject newAppointment = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

            // Stel ID, naam, datum en sticker in
            appointmentData.id = valueManager.GetNewID();
            appointmentData._name = prefab.name;
            appointmentData._date = startDate.AddDays(appointmentPrefabs.IndexOf(prefab) * 5);
            appointmentData._sticker = 0;

            // Log afspraak details
            Debug.Log($"Afspraak aangemaakt: ID={appointmentData.id}, Naam={appointmentData._name}, Datum={appointmentData._date}, Sticker={appointmentData._sticker}");
        }

        // Sorteer afspraken
        if (sorter != null)
        {
            sorter.SortAppointments();
        }
    }
}
