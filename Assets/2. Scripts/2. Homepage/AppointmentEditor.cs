using System;
using TMPro;
using UnityEngine;

/*
public class AppointmentEditor : MonoBehaviour
{
    // UI-elementen voor invoer
    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public TMP_InputField nameCreateInput;
    public TMP_InputField dateCreateInput;

    // Prefab en ouderschap voor afspraken
    public GameObject appointmentPrefab;
    public GameObject queuePrefab;
    public Transform parentTransform;

    // Verwijzingen naar andere scripts
    public ValueManager valueManager;
    public AppointmentSorter sorter;

    //als iemand op een knop drukt dan word er een nieuwe appointment aangemaakt
    public void CreateAppointment()
    {
        // Haal invoer op
        string appointmentName = nameCreateInput.text;
        DateTime appointmentDate = DateTime.Parse(dateCreateInput.text);

        // Instantieer een nieuwe afspraak
        GameObject newAppointment = Instantiate(appointmentPrefab, parentTransform);
        AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

        // Genereer een ID voor de afspraak
        int appointmentId = valueManager.GetNewID();

        // Stel de gegevens in voor de afspraak
        appointmentData.id = appointmentId;
        appointmentData._name = appointmentPrefab.name;
        appointmentData._date = appointmentDate;
        appointmentData._sticker = 0; // Standaard geen sticker
        appointmentData.isQueue = false;

        Debug.Log($"Nieuwe afspraak aangemaakt: ID={appointmentData.id}, Naam={appointmentData._name}, Datum={appointmentData._date}, Sticker={appointmentData._sticker}");

        // Instantieer de bijbehorende queue
        GameObject newQueue = Instantiate(queuePrefab, parentTransform);
        AppointmentData queueData = newQueue.GetComponent<AppointmentData>();

        // Stel de gegevens in voor de queue
        queueData.id = appointmentId;
        queueData._name = queuePrefab.name;
        queueData._date = appointmentDate.AddDays(1); // …Èn dag later
        queueData._sticker = 0;
        appointmentData.isQueue = true;

        Debug.Log($"Nieuwe queue aangemaakt: ID={queueData.id}, Naam={queueData._name}, Datum={queueData._date}, Sticker={queueData._sticker}");

        // Sorteer de afspraken opnieuw
        sorter.SortAppointments();
    }

    //veranderd de data in een al bestaande appointment[datum]
    public void EditAppointment()
    {
        // Haal de nieuwe gegevens op
        string newName = nameInput.text;
        DateTime newDate = DateTime.Parse(dateInput.text);

        // Zoek de juiste afspraak en pas deze aan
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.id == valueManager.selectedPrefab)
            {
                appointment.SetData(newName, newDate, appointment._sticker);
                break;
            }
        }

        // Sorteer opnieuw na bewerking
        sorter.SortAppointments();
    }

    //veranderd de data in een al bestaande appointment[sticker]
    public void EditSticker(int newSticker)
    {
        // Zoek de juiste afspraak en pas de sticker aan
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.id == valueManager.selectedPrefab)
            {
                appointment.SetData(appointment._name, appointment._date, newSticker);
                break;
            }
        }

        // Sorteer opnieuw
        sorter.SortAppointments();
    }

    //verwijderd een appointment
    public void DeleteAppointment()
    {
        int selectedId = valueManager.selectedPrefab;
        Transform appointmentToDelete = null;
        Transform queueToDelete = null;

        // Zoek de afspraak en de bijbehorende queue
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null)
            {
                if (appointment.id == selectedId)
                {
                    appointmentToDelete = child;
                }
                else if (appointment.id == selectedId * -1) // Zoek de queue met ID * -1
                {
                    queueToDelete = child;
                }
            }
        }

        // Verwijder de gevonden afspraak en queue
        if (appointmentToDelete != null)
        {
            Debug.Log($"Afspraak verwijderd: ID={selectedId}");
            Destroy(appointmentToDelete.gameObject);
        }

        if (queueToDelete != null)
        {
            Debug.Log($"Queue verwijderd: ID={selectedId * -1}");
            Destroy(queueToDelete.gameObject);
        }

        // Sorteer opnieuw na verwijdering
        sorter.SortAppointments();
    }

}
*/