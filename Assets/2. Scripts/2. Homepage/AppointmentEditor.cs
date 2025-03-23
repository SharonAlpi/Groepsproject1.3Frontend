using TMPro;
using UnityEngine;

public class AppointmentEditor : MonoBehaviour
{
    [SerializeField] private int stickerInput;

    // UI-elementen voor invoer
    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public TMP_InputField nameCreateInput;
    public TMP_InputField dateCreateInput;

    // Prefab en ouderschap voor afspraken
    public GameObject appointmentPrefab;
    public Transform parentTransform;

    // Verwijzingen naar andere scripts
    public ValueManager valueManager;
    public AppointmentSorter sorter;

    public void CreateAppointment()
    {
        // Haal invoer op
        string appointmentName = nameCreateInput.text;
        int appointmentDate = int.Parse(dateCreateInput.text);

        // Instantieer een nieuwe afspraak
        GameObject newAppointment = Instantiate(appointmentPrefab, parentTransform);
        AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

        // Stel de gegevens in
        appointmentData.id = valueManager.GetNewID();
        appointmentData._name = appointmentName;
        appointmentData._date = appointmentDate;
        appointmentData._sticker = 0; // Standaard geen sticker

        // Sorteer de afspraken opnieuw
        sorter.SortAppointments();

        Debug.Log($"Nieuwe afspraak aangemaakt: ID={appointmentData.id}, Naam={appointmentData._name}, Datum={appointmentData._date}, Sticker={appointmentData._sticker}");
    }

    public void EditAppointment()
    {
        // Haal de nieuwe gegevens op
        string newName = nameInput.text;
        int newDate = int.Parse(dateInput.text);

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

    public void SaveNewData()
    {
        // Loop door alle afspraken en sla ze op (in debug log)
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null)
            {
                Debug.Log($"Afspraak opgeslagen: ID={appointment.id}, Naam={appointment._name}, Datum={appointment._date}, Sticker={appointment._sticker}");
            }
        }

        // Sorteer opnieuw na opslaan
        sorter.SortAppointments();
    }
}
