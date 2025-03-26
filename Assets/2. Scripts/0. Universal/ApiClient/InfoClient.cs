using System;
using System.Net;
using UnityEngine;

public class InfoClient : MonoBehaviour
{
    private string OperationRoute = "/info";

    public async Awaitable<IWebRequestReponse> Getinfo()
    {
        //return new WebRequestData<bool>(false);

        var response = await WebClient.instance.SendGetRequest(OperationRoute);
        switch (response)
        {
            case WebRequestData<string> res:
                var jsonData = JsonUtility.FromJson<Info>(res.Data);
                return new WebRequestData<Info>(jsonData);
            default:
                return response;
        };

    }
}