using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private WebClient client;
    private TokenStoreAndRefresh tokenStorer;

    public TMP_InputField email;
    public TMP_InputField password;

    //TODO: Response opslaan in dto
    public GameObject failedToLogin;
    public GameObject error;
    
    void Start()
    {
        client = GameObject.Find("ApiManager").GetComponent<WebClient>();
        tokenStorer = GameObject.Find("TokenStorer").GetComponent<TokenStoreAndRefresh>();
    }

    public async void Click()
    {
        string email = this.email.text;
        string password = this.password.text;
        Login login = new Login();
        login.email = email;
        login.password = password;
        Debug.Log(login);
        IWebRequestReponse result = await client.SendPostRequest("/account/login", JsonUtility.ToJson(login));
        if (result is WebRequestData<string> dataResponse)
        {
            string responseData = dataResponse.Data;
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(responseData);

            tokenStorer.StoreToken(loginResponse);
            client.SetToken(loginResponse.accessToken);
            SceneManager.LoadScene("ExplanationScene");
            error.SetActive(false);
            failedToLogin.SetActive(false);
        }
        // Handle error case
        else if (result is WebRequestError errorResponse)
        {
            if(errorResponse.ErrorMessage.Contains("401"))
            {
                failedToLogin.SetActive(true);
                error.SetActive(false);
            }
            else
            {
                error.SetActive(true);
                failedToLogin.SetActive(false);
            }
            Debug.Log("Error: " + errorResponse.ErrorMessage);
        }
    }
}
