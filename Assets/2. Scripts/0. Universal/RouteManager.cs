using UnityEngine;
using UnityEngine.SceneManagement;



public class RouteManager : MonoBehaviour
{
    public bool OperationRoute = true;
    public InfoClient RouteClient;

    public async void DecideRoute()
    {
        var response = await RouteClient.Getinfo();
        switch (response)
        {
            case WebRequestData<Info> res:
                if (res.Data.Route)
                {
                    SceneManager.LoadScene("OperationRouteScene", LoadSceneMode.Single);
                }
                else
                {
                    SceneManager.LoadScene("RouteScene", LoadSceneMode.Single);
                }
                break;
            case WebRequestError err:
                Debug.Log(err.ErrorMessage);
                break;
        }
    }

        
}