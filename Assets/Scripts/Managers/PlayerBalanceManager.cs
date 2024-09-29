using UnityEngine;
using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using System.Threading.Tasks;

public class PlayerBalanceManager : MonoBehaviour
{
    public static PlayerBalanceManager Instance { get; private set; }

    public string currencyId = "COINS";  // Currency ID
    public long currentBalance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist this manager across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }
    }

    // Fetch player's current balance from Unity Economy
    public async Task FetchCurrentBalance()
    {
        try
        {
            var balances = await EconomyService.Instance.PlayerBalances.GetBalancesAsync();
            foreach (var balance in balances.Balances)
            {
                if (balance.CurrencyId == currencyId)
                {
                    currentBalance = balance.Balance; // Update current balance
                    break;
                }
            }
        }
        catch (EconomyException e)
        {
            Debug.LogError($"Error fetching balance: {e.ErrorCode} - {e.Message}");
        }
    }
}
