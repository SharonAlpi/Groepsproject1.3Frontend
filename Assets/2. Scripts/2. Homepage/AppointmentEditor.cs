using TMPro;
using UnityEngine;

public class AppointmentEditor : MonoBehaviour
{
    [SerializeField] private int stickerInput;

    public TMP_InputField nameInput;
    public TMP_InputField dateInput;
    public TMP_InputField nameCreateInput;
    public TMP_InputField dateCreateInput;

    public GameObject appointmentPrefab;

    public Transform parentTransform;
    public ValueManager valueManager;
    public AppointmentSorter sorter;
    public void CreateAppointment()
    {
        // Read the name and date from the input fields
        string appointmentName = nameCreateInput.text;
        int appointmentDate = int.Parse(dateCreateInput.text);

        // Create a new appointment prefab
        GameObject newAppointment = Instantiate(appointmentPrefab, parentTransform);

        // Get the AppointmentData component of the new prefab
        AppointmentData appointmentData = newAppointment.GetComponent<AppointmentData>();

        // Set the ID, name, date, and sticker for the new appointment
        // We assume ID is set from the ValueManager, and sticker comes from the stickerInput variable
        appointmentData.id = valueManager.GetNewID(); 
        appointmentData._name = appointmentName;
        appointmentData._date = appointmentDate;
        appointmentData._sticker = 0;

        // Sort appointments to maintain order
        sorter.SortAppointments();

        // Optionally, print/log the new appointment data for confirmation
        Debug.Log($"Created new appointment: ID={appointmentData.id}, Name={appointmentData._name}, Date={appointmentData._date}, Sticker={appointmentData._sticker}");
    }


    public void EditAppointment()
    {
        string newName = nameInput.text;
        int newDate = int.Parse(dateInput.text);

        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.id == valueManager.selectedPrefab)
            {
                appointment.SetData(newName, newDate, appointment._sticker);
                break;
            }
        }

        sorter.SortAppointments();
    }

    public void EditSticker(int newSticker)
    {

        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null && appointment.id == valueManager.selectedPrefab)
            {
                appointment.SetData(appointment._name, appointment._date, newSticker);
                break;
            }
        }

        sorter.SortAppointments();
    }


    public void SaveNewData()
    {
        foreach (Transform child in parentTransform)
        {
            AppointmentData appointment = child.GetComponent<AppointmentData>();
            if (appointment != null)
            {
                Debug.Log($"Saved Appointment: ID={appointment.id}, Name={appointment._name}, Date={appointment._date}, Sticker={appointment._sticker}");
            }
        }

        sorter.SortAppointments();

    }
}