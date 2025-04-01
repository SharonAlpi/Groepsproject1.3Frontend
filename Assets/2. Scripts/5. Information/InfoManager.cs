using System;
using System.Runtime.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InfoManager : MonoBehaviour
{
    public InfoClient infoClient;
    public TMP_InputField childNameInput;
    public TMP_InputField doctorInput;
    public TMP_InputField birthDayInput;
    public Toggle route;


    public async void CreateInfo()
    {
        // verzamel alle info
        var childName = childNameInput.text;
        var doctorName = doctorInput.text;
        var birthDate = birthDayInput.text;
        bool hasRoute = route.isOn;
        // genereer een random avatar
        int avatarId = UnityEngine.Random.Range(1, 3);

        // parse de date
        var date = DateTime.Parse(birthDate);
        Debug.Log(hasRoute);
        // maak het info object aan
        var info = new Info
        {
            name = childName,
            nameDocter = doctorName,
            route = hasRoute,
            avatarId = avatarId,
            birthDay = birthDate
        };
        var response = await infoClient.CreateInfo(info);
        switch (response)
        {
            // het maakt ons alleen maar uit of het fout gegaan is of niet
            case WebRequestError err:
                Debug.Log(err.ErrorMessage);
                break;
            default:
                SceneManager.LoadScene("ExplanationScene", LoadSceneMode.Single);
                break;
        };

    }
}
