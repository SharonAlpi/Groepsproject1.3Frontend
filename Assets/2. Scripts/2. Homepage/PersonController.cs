using UnityEngine;
using UnityEngine.UI; // Nodig voor UI-componenten

public class PersonController : MonoBehaviour
{
    private ValueManager valueManager;
    private AppointmentData parentAppointmentData;

    public bool exists = false;
    public GameObject uiImage; // Sleep hier de UI-afbeelding in via de Inspector

    void Start()
    {
        valueManager = FindFirstObjectByType<ValueManager>();
        parentAppointmentData = GetComponentInParent<AppointmentData>();

        Debug.Log($"parentAppointmentData._date: {parentAppointmentData._date}; valueManager.currentDate: {valueManager.currentDate}; {exists}");

        UpdateUI(); // Zorgt ervoor dat de UI correct wordt bijgewerkt bij start
    }

    void Update()
    {
        UpdateUI(); // Controleer en update UI bij elke frame
    }

    void UpdateUI()
    {
        if (parentAppointmentData._date == valueManager.currentDate && parentAppointmentData.isQueue)
        {
            uiImage.SetActive(true);
        }
        else
        {
            uiImage.SetActive(false);
            if(!parentAppointmentData.isQueue && parentAppointmentData.guidId == valueManager.currentAppoimnetId)
            {
                uiImage.SetActive(true);
            }
        }
    }
}