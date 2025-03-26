using System;
using System.Net;
using UnityEngine;

public class InfoClient : MonoBehaviour
{
    private string OperationRoute = "/Info";

    public async Awaitable<IWebRequestReponse> Getinfo()
    {
        //return new WebRequestData<bool>(false);

        var response = await WebClient.instance.SendGetRequest(OperationRoute+"/GetInfo");
        switch (response)
        {
            case WebRequestData<string> res:
                var jsonData = JsonUtility.FromJson<Info>(res.Data);
                return new WebRequestData<Info>(jsonData);
            default:
                return response;
        };

    }
    public async Awaitable<IWebRequestReponse> Postinfo(Info info)
    {
        var response = await WebClient.instance.SendPostRequest(OperationRoute+"/CreateInfo",JsonUtility.ToJson(info));
        return response;
    }
}