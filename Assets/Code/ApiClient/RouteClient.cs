using UnityEngine;

public class RouteClient : MonoBehaviour
{
    private string OperationRoute = "/user/hasoperation";

    public async Awaitable<IWebRequestReponse> HasOperation()
    {
        return new WebRequestData<bool>(false);

        // return await WebClient.instance.SendGetRequest(OperationRoute);
    }
}