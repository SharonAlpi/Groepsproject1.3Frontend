using System.Linq;
using TMPro;
using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{
    //voor een button om een acount aan te maken
    private WebClient client;

    public TMP_InputField email;
    public TMP_InputField password;

    public GameObject Taken;
    public GameObject BadPassword;
    public GameObject BadEmail;
    public GameObject Error;
    void Start()
    {
        client = GameObject.Find("ApiManager").GetComponent<WebClient>();
    }
    //waneer je een acount aanmaakt
    public async void Click()
    {
        bool containsNonAlphanumeric = false;
        //de gegevens instellen op de ingevoerde gegevens
        string email = this.email.text;
        string password = this.password.text;
        //het wachtwoord moet een alfaneumeric bevatten hier checken we dat
        foreach (char c in password)
        {
            if (!char.IsLetterOrDigit(c))
            {
                containsNonAlphanumeric = true;
                break;
            }
        }
        // als er ook een uppercase lowercase en cijfer inzitten en de lengte minstens 10 is is het wachtwoord geldig
        if (password.Any(char.IsUpper) && password.Any(char.IsDigit) && password.Any(char.IsLower) && containsNonAlphanumeric && password.Length >= 10)
        {
            //de email is een geldige email, als er één @ in zit, en er voor en na de @ text is, verder mogen er geen niet alfanumerics inzitten behalve de punt
            if (email.Contains("@") && email.IndexOf('@') > 0 && email.IndexOf('@') < email.Length - 1 && IsValidEmail(email))
            {
                //de LoginRepository een call laten doen met de email een pasword om te registreren

                Login login = new Login();
                login.email = email;
                login.password = password;
                Debug.Log(login);
                var result = await client.SendPostRequest("/account/register", JsonUtility.ToJson(login));
                //als het resultaat succes was is het aangemaakt en moet je inloggen
                if (result is WebRequestData<string> dataResponse)
                {
                    SceneManager.LoadScene("LoginScene");
                    Taken.SetActive(false);
                }
                //als acount al bestaat error geven
                else if (result is WebRequestError errorResponse)
                {
                    if (errorResponse.ErrorMessage.Contains("400"))
                    {
                        Taken.SetActive(true);
                        Error.SetActive(false);
                    }
                    else
                    {
                        Error.SetActive(true);
                        Taken.SetActive(false);
                    }
                    Debug.Log("Error: " + errorResponse.ErrorMessage);
                }               
                BadEmail.SetActive(false);
                BadPassword.SetActive(false);
            }
            //foutmelding email niet goed
            else
            {
                Taken.SetActive(false);
                BadEmail.SetActive(true);
                BadPassword.SetActive(false);
            }
        }
        //foutmelding wachtwoord niet goed
        else
        {
            Taken.SetActive(false);
            BadEmail.SetActive(false);
            BadPassword.SetActive(true);
        }
    }
    //checken of er 1 @ in zit, er voor en na geen niet alfaneumerics zijn, behalve de punt en de @ zelf
    bool IsValidEmail(string input)
    {

        int atCount = input.Split('@').Length - 1;
        if (atCount != 1)
        {
            return false;
        }

        foreach (char c in input)
        {
            if (c != '@' && c != '.' && !char.IsLetterOrDigit(c))
            {
                return false;
            }
        }
        return true;
    }
}
