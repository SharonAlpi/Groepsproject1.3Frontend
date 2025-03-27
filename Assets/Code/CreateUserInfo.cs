using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CreateUserInfo : MonoBehaviour
{
    private string Username;
    private string doctorName;
    private bool route;
    private DateTime birthday;
    private int avatarId;

    public Info info;
    public InfoClient InfoClient;

    public TMP_Text text;
    public TMP_InputField NameinputField;
    public TMP_Dropdown DoctorDropdown;
    public TMP_InputField DateInputField;
    //public GameObject gameObject; 
    public string[] doctors;
    void Update()
    {
        Username = NameinputField.text;
        doctorName = doctors[DoctorDropdown.value];
    }
    public void change(bool change)
    {   
        route = change; 
        if (change) {  }
        else {}
    }
    public async Task EnterResults()
    {
        birthday = DateTime.Parse(DateInputField.text);
        if (birthday != null || Username == "" || Username == null) { MissingRequirement("Didn't submit all required information"); }
        else
        {
            MissingRequirement("");
            await InfoClient.Postinfo(new Info
            {
                Route = route,
                BirthDay = birthday,
                NameDocter = doctorName,
                Name = Username,
                AvatarId = 1
            });
        }
    }
    public void MissingRequirement(string missing)
    {
        text.text = missing;
    }
}
