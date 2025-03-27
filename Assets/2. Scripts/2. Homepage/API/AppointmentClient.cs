using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;
using static System.Net.WebRequestMethods;

public class AppointmentClient : MonoBehaviour
{
    public string baseUrl = ("https://localhost:7257");

    public async Task<List<Appointment>> GetAllAppointments()
    {
        string url = baseUrl + "/Appointment/GetAllAppointments";
        UnityWebRequest request = UnityWebRequest.Get(url);
        request.SetRequestHeader("Content-Type", "application/json");

        await request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            return JsonUtility.FromJson<List<Appointment>>(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error fetching appointments: " + request.error);
            return null;
        }
    }

    public async Task<bool> CreateAppointment(Appointment appointment)
    {
        string url = baseUrl + "/Appointment/CreateAppointment";
        string json = JsonUtility.ToJson(appointment);
        UnityWebRequest request = UnityWebRequest.Post(url, json, "application/json");

        await request.SendWebRequest();

        return request.result == UnityWebRequest.Result.Success;
    }

public async Task<bool> EditAppointment(Appointment appointment)
{
    string url = baseUrl + "/Appointment/SaveAppointment";
    string json = JsonUtility.ToJson(appointment);
    
    UnityWebRequest request = new UnityWebRequest(url, "PUT");
    byte[] jsonBytes = new System.Text.UTF8Encoding().GetBytes(json);

    request.uploadHandler = new UploadHandlerRaw(jsonBytes);
    request.downloadHandler = new DownloadHandlerBuffer();
    request.SetRequestHeader("Content-Type", "application/json");

    await request.SendWebRequest();

    return request.result == UnityWebRequest.Result.Success;
}


    public async Task<bool> DeleteAppointment(Guid appointmentId)
    {
        string url = baseUrl + "/Appointment/DeleteAppointment?appointmentId=" + appointmentId;
        UnityWebRequest request = UnityWebRequest.Delete(url);

        await request.SendWebRequest();

        return request.result == UnityWebRequest.Result.Success;
    }
}
