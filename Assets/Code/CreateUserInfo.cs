using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CreateUserInfo : MonoBehaviour
{
    private string name;
    private string doctorName;
    private bool route;
    private DateTime birthday;
    private int avatarId;

    public Info info;
    public InfoClient InfoClient;

    public TMP_InputField NameinputField;
    public TMP_Dropdown DoctorDropdown;
    public TMP_InputField DateInputField;
    //public GameObject gameObject; 
    public string[] doctors;
    void Update()
    {
        name = NameinputField.text;
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
        await InfoClient.Postinfo(new Info
        {
            Route = route,
            BirthDay = birthday,
            NameDocter = doctorName,
            Name = name,
            AvatarId = 1
        });
    }
}
