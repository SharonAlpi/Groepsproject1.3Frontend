using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RuntimeController : MonoBehaviour
{
    //verwijzing naar manager scripts
    public AppointmentInstantiator instantiator;
    public ValueManager valueManager;

    //verwijzing naar text
    public TMP_Text prefabName;
    public TMP_Text createName;
    public TMP_Text prefabDate;


    void Start()
    {
        //Appointments 
        instantiator.CreateAppointments();

        //UI -> Panels

        //UI -> Text
        createName.text = "Follow-up";
    }

    // Update is called once per frame
    void Update()
    {
        //Appointmets

        //UI -> Panels

        //UI -> Text
        prefabName.text = valueManager.appointmentName;
        prefabDate.text = valueManager.appointmentDate.ToString();
    }
}
