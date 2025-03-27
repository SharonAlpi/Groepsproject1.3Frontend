using TMPro;
using UnityEngine;

public class LoginAccount : MonoBehaviour
{
    private string Email;
    private string Password;
    public AccountRepository accountRepository;
    public TMP_InputField EmailInputField;
    public TMP_InputField PasswordInputField;
    public TMP_Text text;
    private void Update()
    {
        Email = EmailInputField.text;
        Password = PasswordInputField.text;
    }
    public void Login()
    {
        if (Email == null || Password == null || Email == "" ||Password == "") { MissingRequirement("Email and/or password is missing"); }
        else
        {
            MissingRequirement("");
            accountRepository.Login(Email, Password);
        }
    }

    public void MissingRequirement(string missing)
    {
        text.text = missing;
    }
}
