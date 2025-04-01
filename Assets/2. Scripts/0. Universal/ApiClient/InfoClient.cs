using System;
using System.Net;
using UnityEngine;

public class InfoClient : MonoBehaviour
{
    private string GetRoute = "/Info/GetInfo";
    private string PostRoute = "/Info/CreateInfo";

    public async Awaitable<IWebRequestReponse> Getinfo()
    {
        //return new WebRequestData<bool>(false);

        var response = await WebClient.instance.SendGetRequest(GetRoute);
        switch (response)
        {
            case WebRequestData<string> res:
                Debug.Log(res.Data);
                var jsonData = JsonUtility.FromJson<Info>(res.Data);
                return new WebRequestData<Info>(jsonData);
            default:
                return response;
        };

    }
    public async Awaitable<IWebRequestReponse> CreateInfo(Info info)
    {
        Debug.Log($"Creating info : name{info.name}");
        var data = JsonUtility.ToJson(info);
        Debug.Log($"Serialized Info: {data}");

        // het maakt alleen uit of het goed gegaan is.
        return await WebClient.instance.SendPostRequest(PostRoute, data);
    }
}