using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using System.Threading.Tasks;
using TMPro;

public class AuthManager : MonoBehaviour
{
    public TextMeshProUGUI status;

    async void Start()
    {
        await UnityServices.InitializeAsync();
    }

    public async void SignIn()
    {
        await SignInAnonymous();
    }

    async Task SignInAnonymous()
    {
        try
        {
            await AuthenticationService.Instance.SignInAnonymouslyAsync(); //allows login to unity without credentials
            print("Sign in success");
            print("Player ID:" + AuthenticationService.Instance.PlayerId);
            status.text = "Player ID:" + AuthenticationService.Instance.PlayerId;
        }
        catch (AuthenticationException ex)
        {

            print("Sign in failed");
            Debug.LogException(ex);
        }

        
    }
    
}
