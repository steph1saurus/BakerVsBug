using UnityEngine;
using System.Collections;

using TMPro;


public class BalanceDisplay : MonoBehaviour
{
    public TextMeshProUGUI balanceDisplay;

    void Start()
    {
        try
        {
            PlayerBalanceManager playerBalanceManager = FindObjectOfType<PlayerBalanceManager>();
            // Ensure the balance manager is initialized
            if (playerBalanceManager != null)
            {
                StartCoroutine(ShowBalance(playerBalanceManager));
            }
            else
            {
                throw new System.Exception("PlayerBalanceManager not found in current scene");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("PlayerBalanceManager instance not found." + e.Message);
        }
    }


    private IEnumerator ShowBalance(PlayerBalanceManager playerBalanceManager)
    {
        // Wait until the balance is fetched (assuming FetchCurrentBalance is a coroutine)
        yield return playerBalanceManager.FetchCurrentBalance();

        // Ensure the balance is updated and instance is valid
        if (playerBalanceManager != null)
        {
            Debug.Log("Current Balance: " + playerBalanceManager.currentBalance);
            balanceDisplay.text = playerBalanceManager.currentBalance.ToString();
        }
        else
        {
            Debug.LogError("PlayerBalanceManager instance is null after fetching balance.");
        }

    }
}
