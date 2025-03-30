using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
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
            id = Guid.NewGuid().ToString(),
            name = appointmentName,
            date = appointmentDate.ToString("yyyy-MM-ddTHH:mm:ss"),
            stickerId = 0, // Standaard geen sticker
            userId = valueManager.UsersID // Moet worden vervangen door de echte gebruiker-ID
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
        appointmentData.guidId = new Guid(newAppointment.id);
        appointmentData._name = appointmentPrefab.name;
        appointmentData._date = appointmentDate;
        appointmentData._sticker = 0;
        appointmentData.isQueue = false;

        Debug.Log($"Nieuwe afspraak aangemaakt: ID={appointmentData.guidId}, Naam={appointmentData._name}, Datum={appointmentData._date}");

        // Instantiate queuePrefab met AppointmentData script
        GameObject newQueue = Instantiate(queuePrefab, parentTransform);
        AppointmentData queueData = newQueue.GetComponent<AppointmentData>();

        // Stel de gegevens in voor de queue
        queueData.guidId = new Guid(newAppointment.id);
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
                    id = Guid.NewGuid().ToString(), // Gebruik de juiste ID hier!
                    name = newName,
                    date = newDate.ToString("yyyy-MM-ddTHH:mm:ss"),
                    stickerId = appointment._sticker,
                    userId = valueManager.UsersID // Moet de echte gebruiker-ID zijn
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
                    id = Guid.NewGuid().ToString(), // Gebruik de juiste ID hier!
                    name = appointment._name,
                    date = appointment._date.ToString("yyyy-MM-ddTHH:mm:ss"),
                    stickerId = newSticker,
                    userId = valueManager.UsersID // Moet de echte gebruiker-ID zijn
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
            Debug.Log(valueManager.startDate.AddDays(i * 5));
            Appointment newAppointment = new Appointment
            {
                id = Guid.NewGuid().ToString(),
                name = prefab.name,
                date = valueManager.startDate.AddDays(i * 5).ToString("yyyy-MM-ddTHH:mm:ss"),
                stickerId = 0,
                userId = valueManager.UsersID
            };
            Debug.Log(JsonUtility.ToJson(newAppointment));

            bool success = await appointmentClient.CreateAppointment(newAppointment);
            if (!success)
            {
                Debug.LogError($"Fout bij opslaan van afspraak: {newAppointment.name}");
                continue;
            }
            Debug.Log("Is Continuing");

            // Instantieer een nieuwe afspraak prefab
            GameObject newAppointmentObj = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointmentObj.GetComponent<AppointmentData>();

            // Genereer een ID voor de afspraak
            int appointmentId = valueManager.GetNewID();

            appointmentData.guidId = new Guid(newAppointment.id);
            appointmentData._name = newAppointment.name;
            appointmentData._date = DateTime.Parse(newAppointment.date);
            appointmentData._sticker = newAppointment.stickerId;
            appointmentData.isQueue = false;

            Debug.Log($"Nieuwe afspraak aangemaakt: {appointmentData._name} op {appointmentData._date}");

            // Instantieer queue prefab voor alle afspraken behalve de laatste
            if (i < appointmentPrefabs.Count - 1)
            {
                GameObject newQueueObj = Instantiate(queuePrefab, parentTransform);
                AppointmentData queueData = newQueueObj.GetComponent<AppointmentData>();

                queueData.guidId = new Guid(newAppointment.id);
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
        foreach (Appointment app in appointments)
        {
            GameObject prefab = null;

            switch (app.name)
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
                    Debug.Log(app.name);
                    Debug.LogError($"Geen overeenkomstige prefab gevonden voor afspraak: {app.name}");
                    continue;
            }

            if (prefab == null)
            {
                Debug.LogError($"Prefab is null voor afspraak: {app.name}");
                continue;
            }

            // Instantieer de juiste afspraak prefab
            GameObject newAppointment = Instantiate(prefab, parentTransform);
            AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

            // Genereer een ID voor de afspraak
            int appointmentId = valueManager.GetNewID();

            appointmentData.guidId = new Guid(app.id);
            appointmentData._name = app.name;
            appointmentData._date = DateTime.Parse(app.date);
            appointmentData._sticker = app.stickerId;
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
            .Where(appointment =>
            {
                DateTime appointmentDate;
                // Try to parse the date using the exact format expected (ISO 8601)
                return !string.IsNullOrEmpty(appointment.date) &&
                       DateTime.TryParseExact(appointment.date, "yyyy-MM-ddTHH:mm:ss",
                           System.Globalization.CultureInfo.InvariantCulture,
                           System.Globalization.DateTimeStyles.None,
                           out appointmentDate) &&
                       appointmentDate <= currentDate;
            })
            .OrderByDescending(appointment => appointment.date) // Sort by most recent first
            .ToList();

        if (validAppointments.Count == 0)
        {
            Debug.Log("Geen afspraak gevonden die voor de huidige datum valt.");
            return;
        }

        // Get the most recent valid appointment (the one closest to the current date)
        Appointment lastAppointment = validAppointments[0];

        // Log the last appointment info (for debugging purposes)
        Debug.Log($"Laatste afspraak gevonden: ID={lastAppointment.id}, Datum={lastAppointment.date}");

        // Update the valueManager to use this ID for the next appointment
        valueManager.currentAppoimnetId = new Guid(lastAppointment.id); // Assign the Id from the last appointment
    }


}