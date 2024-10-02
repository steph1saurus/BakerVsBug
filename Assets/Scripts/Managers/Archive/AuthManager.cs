using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Services.Core;
using Unity.Services.Authentication;
using Unity.Services.Economy;
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

            // Check if the player's economy account exists
            await CheckAndCreateEconomyAccount();
        }
        catch (AuthenticationException ex)
        {

            print("Sign in failed");
            Debug.LogException(ex);
        }


    }

    async Task CheckAndCreateEconomyAccount()
    {
        try
        {
            var playerAccount = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
            if (playerAccount == null)
            {
                print("Player economy does not exist. Creating new account");

                await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync("COIN", 100);
    
            }
            else
            {
                print("Player economy exists");
            }
        }
        catch (System.Exception ex)
        {
            print("An error occurred while checking or creating the player economy account.");
            Debug.LogException(ex);
        }
    }

}
