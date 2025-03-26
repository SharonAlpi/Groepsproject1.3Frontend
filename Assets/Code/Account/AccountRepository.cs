using UnityEngine;

public class AccountRepository : MonoBehaviour 
{
    public PageManager _pageManager;
    public void Login(string Email,string Password) {
        _pageManager.Scene2();
    }
    public void Register(string Email,string Password) {
        _pageManager.Scene2();
    }
}
