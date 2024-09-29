using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;
using System;
using System.Threading.Tasks;

public class CurrencyManager : MonoBehaviour
{
    // Function to add or deduct currency
    public async void ModifyCurrency(string currencyId, int amount)
    {
        try
        {
            // Modify the player's balance (positive amount adds, negative deducts)
            var result = await EconomyService.Instance.PlayerBalances.SetBalanceAsync(currencyId, amount);
            Debug.Log($"Currency {currencyId} modified by {amount}. New balance: {result.Balance}");
        }
        catch (EconomyException e)
        {
            Debug.LogError($"Failed to modify currency: {e.ErrorCode} - {e.Message}");
        }
    }

    private void Start()
    {
       
    }

    public async Task SetBalanceAsync(string currencyId, int amount)
    {
        try
        {
            // Call the Economy SDK's ModifyBalanceAsync method
            PlayerBalance balance = await EconomyService.Instance.PlayerBalances.IncrementBalanceAsync(currencyId, amount);

            // Log the new balance in the console
            Debug.Log($"Successfully modified currency: {currencyId}. Amount: {amount}. New balance: {balance.Balance}");
        }
        catch (EconomyException e)
        {
            // Catch any exceptions thrown by the SDK and log error details
            Debug.LogError($"Failed to modify currency: {e.ErrorCode} - {e.Message}");
        }
        catch (Exception ex)
        {
            // General exception handler for any other issues
            Debug.LogError($"Unexpected error occurred: {ex.Message}");
        }
    }
   
}
