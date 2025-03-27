using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Windows;

/// <summary>
/// de id in het model en het id van het scripts zijn andere ids 
/// de id van het model is een guid en wordt gebrukt met de database
/// de id van het AppointmentData heeft te maken met code
/// </summary>
public class AppointmentController : MonoBehaviour
{
    // UI-elementen voor invoer
    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public TMP_InputField nameCreateInput;
    public TMP_InputField dateCreateInput;

    // Prefab en ouderschap voor afspraken
    public List<GameObject> appointmentPrefabs;
    public GameObject appointmentPrefab;
    public GameObject queuePrefab;
    public Transform parentTransform;

    // Verwijzingen naar andere scripts
    public ValueManager valueManager;
    public AppointmentSorter sorter;
    public AppointmentClient appointmentClient;


    /// <summary>
    ///  Maakt een nieuwe appointment aan en slaat het op in de database
    ///  maakt een queue prefab aan na elke appointmentPrefab soorten(Wordt niet opgeslagen!!!!)
    /// </summary>
    public async void CreateAppointment()
    {
        // Haal invoer op
        string appointmentName = nameCreateInput.text;
        DateTime appointmentDate = DateTime.Parse(dateCreateInput.text);

        // Maakt [Serializable] Appointment aan in Database
        Appointment newAppointment = new Appointment
        {
            Id = Guid.NewGuid(),
            Name = appointmentName,
            Date = appointmentDate,
            StickerId = 0, // Standaard geen sticker
            UserId = valueManager.UsersID // Moet worden vervangen door de echte gebruiker-ID
        };

        // Wach totdat info door is gegaan
        bool success = await appointmentClient.CreateAppointment(newAppointment);
        if (!success)
        {
            Debug.LogError("Fout bij het aanmaken van de afspraak in de database.");
            return;
        }

        // Instantiate appointmentPrefab met AppointmentData script
        GameObject newAppointmentObj = Instantiate(appointmentPrefab, parentTransform);
        AppointmentData appointmentData = newAppointmentObj.GetComponent<AppointmentData>();

        // Genereer een ID voor de afspraak
        int appointmentId = valueManager.GetNewID();

        // Stel de gegevens in
        appointmentData.guidId = newAppointment.Id;
        appointmentData._name = appointmentPrefab.name;
        appointmentData._date = appointmentDate;
        appointmentData._sticker = 0;
        appointmentData.isQueue = false;

        Debug.Log($"Nieuwe afspraak aangemaakt: ID={appointmentData.guidId}, Naam={appointmentData._name}, Datum={appointmentData._date}");

        // Instantiate queuePrefab met AppointmentData script
        GameObject newQueue = Instantiate(queuePrefab, parentTransform);
        AppointmentData queueData = newQueue.GetComponent<AppointmentData>();

        // Stel de gegevens in voor de queue
        queueData.guidId = newAppointment.Id;
        queueData._name = queuePrefab.name;
        queueData._date = appointmentDate.AddDays(1);
        queueData._sticker = 0;
        queueData.isQueue = true;

        Debug.Log($"Nieuwe queue aangemaakt: ID={queueData.guidId}, Naam={queueData._name}, Datum={queueData._date}");

        // Sorteer de afspraken opnieuw
        sorter.SortAppointments();

    }

    /// <summary>
    /// Veranderd de naam en datum van een al bestaande object en slaat het op in de database
    /// </summary>
    public async void EditAppointment()
    {
        // Haal de nieuwe gegevens op
        string newName = nameInput.text;
        DateTime newDate = DateTime.Parse(dateInput.text);

        // Zoek de juiste afspraak en update deze
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.guidId == valueManager.selectedPrefab)
            {
                // Update in de database
                Appointment updatedAppointment = new Appointment
                {
                    Id = Guid.NewGuid(), // Gebruik de juiste ID hier!
                    Name = newName,
                    Date = newDate,
                    StickerId = appointment._sticker,
                    UserId = valueManager.UsersID // Moet de echte gebruiker-ID zijn
                };

                bool success = await appointmentClient.EditAppointment(updatedAppointment);
                if (!success)
                {
                    Debug.LogError("Fout bij het bijwerken van de afspraak in de database.");
                    return;
                }

                // Update in Unity
                appointment.SetData(newName, newDate, appointment._sticker);
                Debug.Log($"Afspraak bijgewerkt: ID={appointment.guidId}, Naam={newName}, Datum={newDate}");
                break;
            }
        }

        // Sorteer opnieuw
        sorter.SortAppointments();
    }

    /// <summary>
    /// Veranderd de sticker int van een al bestaande object en slaat het op in de database
    /// </summary>
    public async void EditSticker(int newSticker)
    {
        // Zoek de juiste afspraak en pas de sticker aan
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.guidId == valueManager.selectedPrefab)
            {
                // Update in de database
                Appointment updatedAppointment = new Appointment
                {
                    Id = Guid.NewGuid(), // Gebruik de juiste ID hier!
                    Name = appointment._name,
                    Date = appointment._date,
                    StickerId = newSticker,
                    UserId = valueManager.UsersID // Moet de echte gebruiker-ID zijn
                };

                bool success = await appointmentClient.EditAppointment(updatedAppointment);
                if (!success)
                {
                    Debug.LogError("Fout bij het bijwerken van de sticker in de database.");
                    return;
                }

                // Update in Unity
                appointment.SetData(appointment._name, appointment._date, newSticker);
                Debug.Log($"Sticker aangepast: ID={appointment.guidId}, Nieuwe sticker={newSticker}");
                break;
            }
        }

        // Sorteer opnieuw
        sorter.SortAppointments();
    }

    /// <summary>
    /// Verwijderd een object in de game en in de database
    /// Verwijderd queue object met hetzelde id
    /// </summary>
    public async void DeleteAppointment()
    {
        Guid selectedId = valueManager.selectedPrefab;
        Transform appointmentToDelete = null;
        Transform queueToDelete = null;

        // Zoek de afspraak en de queue
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null)
            {
                if (appointment.guidId == selectedId)
                {
                    appointmentToDelete = child;
                }
                else if (appointment.guidId == selectedId) // Zoek de queue
                {
                    queueToDelete = child;
                }
            }
        }

        if (appointmentToDelete != null)
        {
            // Verwijder uit de database
            bool success = await appointmentClient.DeleteAppointment(selectedId); // Gebruik de juiste ID hier!
            if (!success)
            {
                Debug.LogError($"Fout bij het verwijderen van afspraak {selectedId} uit de database.");
                return;
            }

            // Verwijder uit Unity
            Debug.Log($"Afspraak verwijderd: ID={selectedId}");
            Destroy(appointmentToDelete.gameObject);
        }

        if (queueToDelete != null)
        {
            Debug.Log($"Queue verwijderd: ID={selectedId}");
            Destroy(queueToDelete.gameObject);
        }

        // Sorteer opnieuw
        sorter.SortAppointments();
    }


    /// <summary>
    /// Laadt de afspraken
    /// </summary>
    public async void LoadAppointments()
    {
        List<Appointment> existingAppointments = await appointmentClient.GetAllAppointments();

        if (existingAppointments == null || existingAppointments.Count == 0)
        {
            Debug.Log("Geen afspraken in de database, laad nieuwe afspraken.");
            LoadNewAppointments();
        }
        else
        {
            Debug.Log("Afspraken gevonden in de database, laad bestaande afspraken.");
            LoadDataAppointments(existingAppointments);
        }
    }

    /// <summary>
    /// Gaat door de lijst van `appointmentPrefabs` en initialiseert 1 van elke.
    /// Slaat alle aangemaakte afspraken op in de database.
    /// </summary>
    private async void LoadNewAppointments()
    {
        for (int i = 0; i < appointmentPrefabs.Count; i++)
        {
            var prefab = appointmentPrefabs[i];

            // Maak een nieuw `Appointment` object voor de database
            Appointment newAppointment = new Appointment
            {
                Id = Guid.NewGuid(),
                Name = prefab.name,
                Date = valueManager.startDate.AddDays(i * 5),
                StickerId = 0,
                UserId = valueManager.UsersID
            };

            bool success = await appointmentClient.CreateAppointment(newAppointment);
            if (!success)
            {
                Debug.LogError($"Fout bij opslaan van afspraak: {newAppointment.Name}");
                continue;
            }
            Debug.Log("Is Continuing");

            // Instantieer een nieuwe afspraak prefab
            GameObject newAppointmentObj = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointmentObj.GetComponent<AppointmentData>();

            // Genereer een ID voor de afspraak
            int appointmentId = valueManager.GetNewID();

            appointmentData.guidId = newAppointment.Id;
            appointmentData._name = newAppointment.Name;
            appointmentData._date = newAppointment.Date;
            appointmentData._sticker = newAppointment.StickerId;
            appointmentData.isQueue = false;

            Debug.Log($"Nieuwe afspraak aangemaakt: {appointmentData._name} op {appointmentData._date}");

            // Instantieer queue prefab voor alle afspraken behalve de laatste
            if (i < appointmentPrefabs.Count - 1)
            {
                GameObject newQueueObj = Instantiate(queuePrefab, parentTransform);
                AppointmentData queueData = newQueueObj.GetComponent<AppointmentData>();

                queueData.guidId = newAppointment.Id;
                queueData._name = appointmentData._name;
                queueData._date = appointmentData._date.AddDays(1);
                queueData._sticker = 0;
                queueData.isQueue = true;

                Debug.Log($"Queue aangemaakt voor {queueData._name} op {queueData._date}");
            }
        }

        sorter.SortAppointments();
    }

    /// <summary>
    /// Gaat door de database en initialiseert alle `appointmentPrefabs` met een switch-case.
    /// </summary>
    private async void LoadDataAppointments(List<Appointment> appointments)
    {
        foreach (var app in appointments)
        {
            GameObject prefab = null;

            switch (app.Name)
            {
                case "Aankomst":
                    prefab = appointmentPrefabs[0];
                    break;
                case "Follow-up":
                    prefab = appointmentPrefabs[1];
                    break;
                case "Gipsafname":
                    prefab = appointmentPrefabs[2];
                    break;
                case "Nazorg":
                    prefab = appointmentPrefabs[3];
                    break;
                case "Thuis":
                    prefab = appointmentPrefabs[4];
                    break;
                default:
                    Debug.LogError($"Geen overeenkomstige prefab gevonden voor afspraak: {app.Name}");
                    continue;
            }

            if (prefab == null)
            {
                Debug.LogError($"Prefab is null voor afspraak: {app.Name}");
                continue;
            }

            // Instantieer de juiste afspraak prefab
            GameObject newAppointment = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

            // Genereer een ID voor de afspraak
            int appointmentId = valueManager.GetNewID();

            appointmentData.guidId = app.Id;
            appointmentData._name = app.Name;
            appointmentData._date = app.Date;
            appointmentData._sticker = app.StickerId;
            appointmentData.isQueue = false;

            Debug.Log($"Afspraken geladen: {appointmentData._name} op {appointmentData._date}");

            // Instantieer queue prefab
            GameObject newQueueObj = Instantiate(queuePrefab, parentTransform);
            AppointmentData queueData = newQueueObj.GetComponent<AppointmentData>();

            queueData.guidId = appointmentData.guidId;
            queueData._name = appointmentData._name;
            queueData._date = appointmentData._date.AddDays(1);
            queueData._sticker = 0;
            queueData.isQueue = true;

            Debug.Log($"Queue geladen voor {queueData._name} op {queueData._date}");
        }

        FindLastAppointmentDate();
        sorter.SortAppointments();
    }

    /// <summary>
    /// 
    /// </summary>
    public async void FindLastAppointmentDate()
    {
        // Get all appointments from the database
        List<Appointment> appointments = await appointmentClient.GetAllAppointments();

        if (appointments == null || appointments.Count == 0)
        {
            Debug.Log("Geen afspraken gevonden.");
            return;
        }

        // Get the current date (this would be based on the system date or a given value)
        DateTime currentDate = DateTime.Now;

        // Filter appointments to only those that are before or on the current date
        List<Appointment> validAppointments = appointments
            .Where(appointment => appointment.Date <= currentDate)
            .OrderByDescending(appointment => appointment.Date) // Sort by most recent first
            .ToList();

        if (validAppointments.Count == 0)
        {
            Debug.Log("Geen afspraak gevonden die voor de huidige datum valt.");
            return;
        }

        // Get the most recent valid appointment (the one closest to the current date)
        Appointment lastAppointment = validAppointments[0];

        // Log the last appointment info (for debugging purposes)
        Debug.Log($"Laatste afspraak gevonden: ID={lastAppointment.Id}, Datum={lastAppointment.Date}");

        // Update the valueManager to use this ID for the next appointment
        valueManager.currentAppoimnetId = lastAppointment.Id; // Assign the Id from the last appointment
    }


}