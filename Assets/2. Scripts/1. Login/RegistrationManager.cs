using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegistrationManager : MonoBehaviour
{
    //voor een button om een acount aan te maken
    private ApiClient client;

    public TMP_InputField email;
    public TMP_InputField password;

    public GameObject Taken;
    public GameObject BadPassword;
    public GameObject BadEmail;
    public GameObject Error;
    void Start()
    {
        client = GameObject.Find("ApiManager").GetComponent<ApiClient>();
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
                string result = await client.PerformApiCall("/account/register", "Post", JsonUtility.ToJson(login));
                Debug.Log(result);
                //als het resultaat succes was is het aangemaakt en moet je inloggen
                if (result != null)
                {
                    SceneManager.LoadScene("LoginScene");
                    Taken.SetActive(false);
                }
                //als het geen goed resultaat was is het acount al bezet
                else if (result == null)
                {
                    Taken.SetActive(true);
                }
                //als het error was is er iets mis met de database waarschijnlijk azure sloom (0)^(0)
                else if (result == "Error")
                {
                    Error.SetActive(true);
                    Taken.SetActive(false);
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
