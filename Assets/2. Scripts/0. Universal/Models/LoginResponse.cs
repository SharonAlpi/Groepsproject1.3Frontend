using System;
using UnityEngine;
[Serializable]
public class LoginResponse
{
    public string accessToken;
    public int expiresIn;
    public string refreshToken;
}
