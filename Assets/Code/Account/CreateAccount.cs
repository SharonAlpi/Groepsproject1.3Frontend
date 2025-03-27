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
    public TMP_Text text;

    private void Update()
    {
        Email = EmailInputField.text;
        Password = PasswordInputField.text;
    }
    public void Register()
    {
        if (Email == null ||Password == null || Email == "" || Password == "") { MissingRequirement("Email and/or password is missing"); }
        else { 
            MissingRequirement("");
            accountRepository.Register(Email, Password);
        }
    }
    public void MissingRequirement(string missing)
    {
        text.text = missing;
    }
}
