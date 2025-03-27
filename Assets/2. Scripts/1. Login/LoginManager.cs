using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginManager : MonoBehaviour
{
    private ApiClient client;
    private TokenStoreAndRefresh tokenStorer;

    public TMP_InputField email;
    public TMP_InputField password;

    //TODO: Response opslaan in dto
    public GameObject failedToLogin;
    public GameObject error;
    
    void Start()
    {
        client = GameObject.Find("ApiManager").GetComponent<ApiClient>();
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
        string result = await client.PerformApiCall("/account/login", "Post", JsonUtility.ToJson(login));
        Debug.Log(result);
        //als het resultaat goed is mag je inloggen
        if (result != null)
        {
            Debug.Log("Wat Gestored Wordt: " + JsonUtility.FromJson<LoginResponse>(result));
            tokenStorer.StoreToken(JsonUtility.FromJson<LoginResponse>(result));
            SceneManager.LoadScene("ExplanationScene");
            error.SetActive(false);
            failedToLogin.SetActive(false);
        }
        //als de database niet reageert dan krijg je error te zien
        else if(result == "error")
        {
            error.SetActive(true);
            failedToLogin.SetActive(false);
        }
        //als je gegevens fout zijn dan krijg je een error.
        else
        {
            error.SetActive(false);
            failedToLogin.SetActive(true);
        }
    }
}
