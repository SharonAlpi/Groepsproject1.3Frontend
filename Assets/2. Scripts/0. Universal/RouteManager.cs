using UnityEditorInternal;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class RouteManager : MonoBehaviour
{
    public bool OperationRoute = false;
    public InfoClient RouteClient;
    public GameObject info;
    public GameObject route;
    public async void DecideRoute()
    {
        var response = await RouteClient.Getinfo();
        switch (response)
        {          
            case WebRequestData<Info> res:
                Debug.Log(res.Data.route);
                if (!res.Data.route)
                {                 
                    SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
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


    public void PlaceholderDecideRoute()
    {
        if (OperationRoute == true)
        {
            SceneManager.LoadScene("OperationRouteScene", LoadSceneMode.Single);
        }
        else
        {
            SceneManager.LoadScene("HomeScene", LoadSceneMode.Single);
        }

    }
    private void Start()
    {
        GetInfo();
    }

    private async void GetInfo()
    {
        var response = await RouteClient.Getinfo();
        if (response is WebRequestData<Info> result)
        {
            if(result.Data != null)
            {
                route.SetActive(true);
            }
            else
            {
                info.SetActive(true);
            }           
        }
    }
}