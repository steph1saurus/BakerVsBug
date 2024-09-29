using UnityEngine;
using System.Collections;
using TMPro;


public class BalanceDisplay : MonoBehaviour
{
    public TextMeshProUGUI balanceDisplay;

    void Start()
    {
        // Ensure the balance manager is initialized
        if (PlayerBalanceManager.Instance != null)
        {
            StartCoroutine(ShowBalance());
        }
    }

    private IEnumerator ShowBalance()
    {
        // Wait until the balance is fetched
        yield return PlayerBalanceManager.Instance.FetchCurrentBalance();

        // Display current balance
        Debug.Log("Current Balance: " + PlayerBalanceManager.Instance.currentBalance);
        balanceDisplay.text = PlayerBalanceManager.Instance.currentBalance.ToString();

    }
}
