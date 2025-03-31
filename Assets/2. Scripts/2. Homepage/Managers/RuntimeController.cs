using TMPro;
using Unity.VisualScripting;
using UnityEditor.PackageManager;
using UnityEngine;

public class RuntimeController : MonoBehaviour
{
    //verwijzing naar manager scripts
    public ValueManager valueManager;
    public AppointmentController appointmentController;

    //verwijzing naar text
    public TMP_Text prefabName;
    public TMP_Text createName;
    public TMP_Text prefabDate;

    WebClient _client;
    void Start()
    {
        _client = GameObject.Find("ApiManager").GetComponent<WebClient>();
        //Appointments 
        Login();

        //UI -> Panels

        //UI -> Text
        createName.text = "Follow-up";
    }

    //Temporary
    async void Login()
    {
        Login login = new Login();
        login.email = "jurjur@c";
        login.password = "JurJur6269#";
        IWebRequestReponse result = await _client.SendPostRequest("/account/login", JsonUtility.ToJson(login));

        if (result is WebRequestData<string> dataResponse)
        {
            string responseData = dataResponse.Data;
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseData);

            _client.SetToken(loginResponse.accessToken);
        }
        appointmentController.LoadAppointments();
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
