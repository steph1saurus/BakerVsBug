using UnityEngine;
using System.Collections;
using TMPro;

public class BalanceDisplay : MonoBehaviour
{
    public TextMeshProUGUI balanceDisplay;

    private BakedGoodPayoutManager bakedGoodPayoutManager; // Reference to BakedGoodPayoutManager

    void Start()
    {
        try
        {
            bakedGoodPayoutManager = FindObjectOfType<BakedGoodPayoutManager>();

            // Ensure the BakedGoodPayoutManager is initialized
            if (bakedGoodPayoutManager != null)
            {
                // Initial display of the balance
                UpdateBalanceDisplay(bakedGoodPayoutManager.playerCurrencyBalance);
            }
            else
            {
                throw new System.Exception("BakedGoodPayoutManager not found in current scene");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error initializing BalanceDisplay: " + e.Message);
        }
    }

    // Method to update the balance display
    private void UpdateBalanceDisplay(int balance)
    {
        Debug.Log("Current Balance: " + balance);
        balanceDisplay.text = balance.ToString();
    }

    // Call this method to refresh the balance display after completing a level
    public void RefreshBalanceDisplay()
    {
        if (bakedGoodPayoutManager != null)
        {
            UpdateBalanceDisplay(bakedGoodPayoutManager.playerCurrencyBalance);
        }
        else
        {
            Debug.LogError("BakedGoodPayoutManager instance is null while refreshing balance.");
        }
    }
}
