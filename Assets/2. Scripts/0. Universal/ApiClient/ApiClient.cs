using UnityEngine;
using System;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.Networking;
using System.Collections;
public class ApiClient : MonoBehaviour
{
    //de client om calls te doen naar de API
    public static ApiClient instance { get; private set; }
    //deze aanpassen naar je eigen
    private string baseUrl = "https://localhost:7257";
    private string baseUrlAuth;

    private string Token;

    //zorgen dat we altijd blijven bestaan
    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this);
    }

    //een call doen naar de api met een request
    public async Task<string> PerformApiCall(string url, string method, string jsonData = null, string token = null)
    {
        url = baseUrl + url;
        Debug.Log(url);
        using (UnityWebRequest request = new UnityWebRequest(url, method))
        {
            if (!string.IsNullOrEmpty(jsonData))
            {
                byte[] jsonToSend = Encoding.UTF8.GetBytes(jsonData);
                request.uploadHandler = new UploadHandlerRaw(jsonToSend);
            }

            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");

            if (!string.IsNullOrEmpty(token))
            {
                request.SetRequestHeader("Authorization", "Bearer " + token);
            }

            await request.SendWebRequest();
            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.text;
            }
            else if(request.responseCode == 500)
            {
                return "Error";
            }
            else
            {
                return null;
            }
        }
    }
}