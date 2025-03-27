using UnityEngine;
using System.Collections;
using UnityEditor.PackageManager;
using UnityEditor.Experimental.GraphView;
public class TokenStoreAndRefresh : MonoBehaviour
{
    private LoginResponse _loginResponse;

    private ApiClient _client;

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
        _client = GameObject.Find("ApiManager").GetComponent<ApiClient>();
    }

    public void StoreToken(LoginResponse loginresponse)
    {
        _loginResponse = loginresponse;
        StartCoroutine(RefreshTokenAftherTime());
    }
    //token refreshen elke refreshtijd aantal seconden

    private async void RefreshToken()
    {
        string result = await _client.PerformApiCall("/account/refresh", "Post", JsonUtility.ToJson(_loginResponse));
        _loginResponse = JsonUtility.FromJson<LoginResponse>(result);
        Debug.Log(result);
    }
    private IEnumerator RefreshTokenAftherTime()
    {
        yield return new WaitForSeconds(_loginResponse.expiresIn - 40f);
        RefreshToken();
        StartCoroutine(RefreshTokenAftherTime());
    }
}
