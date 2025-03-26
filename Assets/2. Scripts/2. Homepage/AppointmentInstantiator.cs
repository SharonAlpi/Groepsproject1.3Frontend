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

    // Verwijzing naar de AppointmentSorter voor het sorteren van afspraken
    public ValueManager valueManager;
    public AppointmentSorter sorter;

    // Prefab voor de queue die wordt aangemaakt samen met de afspraak
    public GameObject queuePrefab;

    // Maakt afspraken aan op basis van de prefab-lijst
    public void CreateAppointments()
    {
        for (int i = 0; i < appointmentPrefabs.Count; i++)
        {
            var prefab = appointmentPrefabs[i];

            // Maak nieuwe afspraakinstantie
            GameObject newAppointment = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

            // Stel ID, naam, datum en sticker in voor de afspraak
            appointmentData.id = valueManager.GetNewID();
            appointmentData._name = prefab.name;
            appointmentData._date = valueManager.startDate.AddDays(i * 5);
            appointmentData._sticker = 0;
            appointmentData.isQueue = false;

            // Log afspraak details
            Debug.Log($"Afspraak aangemaakt: ID={appointmentData.id}, Naam={appointmentData._name}, Datum={appointmentData._date}, Sticker={appointmentData._sticker}");

            // Controleer of dit niet de laatste afspraak is voordat we de queue aanmaken
            if (i < appointmentPrefabs.Count - 1)
            {
                // Instantieer de bijbehorende queue
                GameObject newQueue = Instantiate(queuePrefab, parentTransform);
                AppointmentData queueData = newQueue.GetComponent<AppointmentData>();

                // Stel gegevens in voor de queue
                queueData.id = appointmentData.id; // ID * -1
                queueData._name = appointmentData._name;
                queueData._date = appointmentData._date.AddDays(1); // 1 dag later
                queueData._sticker = 0;
                appointmentData.isQueue = true;

                // Log queue details
                Debug.Log($"Queue aangemaakt: ID={queueData.id}, Naam={queueData._name}, Datum={queueData._date}, Sticker={queueData._sticker}");
            }
        }

        // Sorteer afspraken
        if (sorter != null)
        {
            sorter.SortAppointments();
        }
    }
}
