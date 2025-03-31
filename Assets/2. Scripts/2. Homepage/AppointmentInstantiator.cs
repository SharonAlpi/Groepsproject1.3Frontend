using UnityEngine;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
/*
public class AppointmentInstantiator : MonoBehaviour
{
    //de soorten prefabs
    public List<GameObject> appointmentPrefabs;

    //de grid waaronder het spwant
    public Transform parentTransform;

    //een globale manager van values
    public ValueManager valueManager;

    //sorteert alle appointments
    public AppointmentSorter sorter;

    //appoinments
    public AppointmentClient appointmentClient;

    //wachtrij objecten (worden met appoinments gespawned maar worden niet opgeslagen)
    public GameObject queuePrefab;

    //als er nog geen appointments zijn in de database maakt het een paar preset appointments door van elke soort prefab 1x aan te maaken
    public async void LoadNewAppointments()
    {
        for (int i = 0; i < appointmentPrefabs.Count; i++)
        {
            var prefab = appointmentPrefabs[i];

            GameObject newAppointment = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

            appointmentData.id = valueManager.GetNewID();
            appointmentData._name = prefab.name;
            appointmentData._date = valueManager.startDate.AddDays(i * 5);
            appointmentData._sticker = 0;
            appointmentData.isQueue = false;

            Debug.Log($"Afspraak aangemaakt: ID={appointmentData.id}, Naam={appointmentData._name}, Datum={appointmentData._date}");

            if (i < appointmentPrefabs.Count - 1)
            {
                GameObject newQueue = Instantiate(queuePrefab, parentTransform);
                AppointmentData queueData = newQueue.GetComponent<AppointmentData>();

                queueData.id = appointmentData.id; // Unieke ID voor de queue
                queueData._name = appointmentData._name;
                queueData._date = appointmentData._date.AddDays(1);
                queueData._sticker = 0;
                queueData.isQueue = true;

                Debug.Log($"Queue aangemaakt: ID={queueData.id}, Naam={queueData._name}, Datum={queueData._date}");
            }
        }

        sorter.SortAppointments();
    }

    //als er al wel appointments zijn dan laad het de appointments die al bestaan in de database
    public async void LoadAppointments()
    {
        //TODO: moet nog appointments moet laden
    }

}
*/