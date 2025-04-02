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
    public GameObject warningBirthDay;
    public GameObject warningName;
    public GameObject warningNameDocter;
    public Button[] avatars;
    private int avatarId;
    public async void CreateInfo()
    {
        // verzamel alle info
        var childName = childNameInput.text;
        var doctorName = doctorInput.text;
        var birthDate = birthDayInput.text;
        bool hasRoute = route.isOn;
        // genereer een random avatar

        DateTime date;
        if (childName != String.Empty)
        {
            if (doctorName != String.Empty)
            {
                // tryparse de date
                if (DateTime.TryParse(birthDate, out date))
                {
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
                else
                {
                    warningBirthDay.SetActive(true);
                }
            }
            else
            {
                warningNameDocter.SetActive(true);
            }
        }
        else
        {
            warningName.SetActive(true);
        }
        
    }

    public void SetAvatar(int avatarId)
    {
        this.avatarId = avatarId;
    }

    private void Update()
    {
        foreach(Button avatar in avatars)
        {
            avatar.interactable = true;
        }
        avatars[avatarId].interactable = false;
    }
}
