using UnityEngine;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using System.Threading.Tasks;

public class PlayerBalanceManager : MonoBehaviour
{
    //public static PlayerBalanceManager Instance { get; private set; }

    //public string currencyId = "COIN";  // Currency ID
    //public long currentBalance;
    //private const int initialBalance = 100; // Initial balance for first-time players


    
    //// Fetch player's current balance from Unity Economy
    //public async Task FetchCurrentBalance()
    //{
    //    try
    //    {
    //        var balances = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
    //        bool hasBalance = false;
    //        foreach (var balance in balances.Balances)
    //        {
    //            if (balance.CurrencyId == currencyId)
    //            {
    //                currentBalance = balance.Balance; // Update current balance
    //                hasBalance = true;
    //                break;
    //            }
    //        }
    //        if (!hasBalance)
    //        {
    //            await SetInitialBalance();
    //        }
    //    }
    //    catch (EconomyException e)
    //    {
    //        Debug.LogError($"Error fetching balance: {e.ErrorCode} - {e.Message}");
    //    }
    //}

    //// Set the initial balance for first-time players
    //private async Task SetInitialBalance()
    //{
    //    try
    //    {
    //        await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, initialBalance);
    //        currentBalance = initialBalance;  // Update local balance
    //        Debug.Log($"Initial balance of {initialBalance} coins set for new player.");
    //    }
    //    catch (EconomyException e)
    //    {
    //        Debug.LogError($"Error setting initial balance: {e.ErrorCode} - {e.Message}");
    //    }
    //}
}
