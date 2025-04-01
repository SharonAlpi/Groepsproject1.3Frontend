using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class RuntimeController : MonoBehaviour
{
    //verwijzing naar manager scripts
    public ValueManager valueManager;
    public AppointmentController appointmentController;
    public InfoClient infoClient;

    //verwijzing naar text
    public TMP_Text prefabName;
    public TMP_Text createName;
    public TMP_Text prefabDate;

    WebClient _client;
    void Start()
    {
        //Appointments 
        appointmentController.LoadAppointments();
        SetInfo();

        //UI -> Panels

        //UI -> Text
        createName.text = "Follow-up";
    }

    async void SetInfo()
    {
        var response = await infoClient.Getinfo();
        if(response is WebRequestData<Info> dataResponse)
        {
            valueManager.currentPerson = dataResponse.Data.avatarId;
            Debug.Log(dataResponse.Data.avatarId);
        }
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
