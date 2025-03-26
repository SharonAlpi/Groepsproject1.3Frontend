using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateAccount: MonoBehaviour
{
    private string Email {  get; set; }
    private string Password {  get; set; }
    public AccountRepository accountRepository;
    public TMP_InputField EmailInputField;
    public TMP_InputField PasswordInputField;

    private void Update()
    {
        Email = EmailInputField.text;
        Password = PasswordInputField.text;
    }
    public void Register()
    {
        if (Email == null ||Password == null) { }
        else { 
            accountRepository.Register(Email, Password);
        }
    }
}
