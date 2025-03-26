using TMPro;
using UnityEngine;

public class LoginAccount : MonoBehaviour
{
    private string Email;
    private string Password;
    public AccountRepository accountRepository;
    public TMP_InputField EmailInputField;
    public TMP_InputField PasswordInputField;
    private void Update()
    {
        Email = EmailInputField.text;
        Password = PasswordInputField.text;
    }
    public void Login()
    {
        if (Email == null || Password == null) { }
        else
        {
            accountRepository.Login(Email, Password);
        }
    }
}
