using UnityEngine;
using UnityEngine.SceneManagement;



public class RouteManager : MonoBehaviour
{
    public bool OperationRoute = true;
    public RouteClient RouteClient;

    public async void DecideRoute()
    {
        var response = await RouteClient.HasOperation();
        switch (response)
        {
            case WebRequestData<bool> res:
                if (res.Data)
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