using System;
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

    public TMP_InputField NameinputField;
    public TMP_Dropdown DoctorDropdown;
    public TMP_InputField DateInputField;
    //public GameObject gameObject;
    public string[] doctors;
    void Update()
    {
        name = NameinputField.text;
        doctorName = doctors[DoctorDropdown.value];
        birthday = DateTime.Parse(DateInputField.text);
    }
    public void change(bool change)
    {   
        route = change; 
        if (change) {  }
        else {}
    }
    public void EnterResults()
    {
        info.Route = route;
        info.BirthDay = birthday;
        info.NameDocter = doctorName;
        info.Name = name;
    }
}
