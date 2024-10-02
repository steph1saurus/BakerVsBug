using Unity.Services.Economy;
using Unity.Services.Economy.Model;
using UnityEngine;
using System;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class CurrencyManager : MonoBehaviour
{
    private void Start()
    {
        GetCurrencyBalance();
    }

    // Function to add or deduct currency
    public void ModifyCurrency(int amount)
    {
        int currentBalance = PlayerPrefs.GetInt("CurrencyBalance", 0);
        currentBalance += amount;

        PlayerPrefs.SetInt("CurrencyBalance", currentBalance);
        PlayerPrefs.Save();
    }

    public int GetCurrencyBalance()
    {
        return PlayerPrefs.GetInt("CurrencyBalance", 0);
    }

    
    // Function to calculate and update currency based on remaining baked goods
    public async Task UpdateCurrencyOnLevelComplete()
    {
        // Wait for the level to be completed by checking GameManager levelCompleteBool
        while (!GameManager.instance.levelCompleteBool)
        {
            await Task.Yield(); // Await until levelCompleteBool becomes true
        }

        // Get all baked goods with the tag "BakedGood"
        GameObject[] bakedGoods = GameObject.FindGameObjectsWithTag("BakedGood");

        int totalPayout = 0;

        // Iterate through each baked good and add its payout to the total
        foreach (GameObject bakedGood in bakedGoods)
        {
            BakedGoodHealth bakedGoodHealth = bakedGood.GetComponent<BakedGoodHealth>();

            if (bakedGoodHealth != null)
            {
                // Use the payout amount from the BakedGoodHealth script
                totalPayout += bakedGoodHealth.levelPayout;
            }
        }

        ModifyCurrency(totalPayout);
    }

}
