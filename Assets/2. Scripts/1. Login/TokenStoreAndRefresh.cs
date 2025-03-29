using UnityEngine;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEditor.Experimental.GraphView;
public class TokenStoreAndRefresh : MonoBehaviour
{
    private LoginResponse _loginResponse;

    private WebClient _client;

    public static TokenStoreAndRefresh instance { get; private set; }
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

    private void Start()
    {
        _client = GameObject.Find("ApiManager").GetComponent<WebClient>();
    }

    public void StoreToken(LoginResponse loginresponse)
    {
        _loginResponse = loginresponse;
        StartCoroutine(RefreshTokenAftherTime());
    }
    //token refreshen elke refreshtijd aantal seconden

    private async void RefreshToken()
    {
        var result = await _client.SendPostRequest("/account/refresh", JsonUtility.ToJson(_loginResponse));
        if (result is WebRequestData<string> dataResponse)
        {
            string responseData = dataResponse.Data;
            _loginResponse = JsonUtility.FromJson<LoginResponse>(responseData);
            _client.SetToken(_loginResponse.accessToken);
            Debug.Log(dataResponse.Data);
        }
    }
    private IEnumerator RefreshTokenAftherTime()
    {
        yield return new WaitForSeconds(_loginResponse.expiresIn - 40f);
        RefreshToken();
        StartCoroutine(RefreshTokenAftherTime());
    }
}
