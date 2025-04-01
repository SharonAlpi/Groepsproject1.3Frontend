using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class AppointmentClient : MonoBehaviour
{
    WebClient _client;
    public string baseUrl = ("https://localhost:7257");

    private void Awake()
    {
        _client = GameObject.Find("ApiManager").GetComponent<WebClient>();      
    }

    public async Task<List<Appointment>> GetAllAppointments()
    {
        var response = await _client.SendGetRequest("/Appointment/GetAllAppointments");
        
        if (response is WebRequestData<string> dataResponse)
        {
            Debug.Log(dataResponse.Data);
            return JsonHelper.ParseJsonArray<Appointment>(dataResponse.Data);
        }
        return new List<Appointment>();
    }

    public async Task<bool> CreateAppointment(Appointment appointment)
    {
        var result = await _client.SendPostRequest("/Appointment/CreateAppointment", JsonUtility.ToJson(appointment));
        if(result is WebRequestData<string> dataResponse)
        {
            return true;
        }
        return false;
    }

    public async Task<bool> EditAppointment(Appointment appointment)
    {
        var result = await _client.SendPutRequest("/Appointment/SaveAppointment", JsonUtility.ToJson(appointment));
        if (result is WebRequestData<string> dataResponse)
        {
            return true;
        }
        return false;
    }


    public async Task<bool> DeleteAppointment(Guid appointmentId)
    {
        var result = await _client.SendDeleteRequest("/Appointment/DeleteAppointment?appointmentId=" + appointmentId);
        if (result is WebRequestData<string> dataResponse)
        {
            return true;
        }
        return false;
    }
}
