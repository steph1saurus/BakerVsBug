using UnityEngine;
using System.Collections.Generic;

public class BakedGoodPayoutManager : MonoBehaviour
{
    // List to store references to the BakedGoodPrefabs in the scene
    public List<GameObject> bakedGoodPrefabs; // Change to List for multiple references

    // Variables to store initial and current life points for each BakedGood
    private List<float> initialLifePoints = new List<float>();
    private List<float> currentLifePoints = new List<float>();

    // Reference to the Player's currency balance
    public int playerCurrencyBalance;

    private void Start()
    {
        // Retrieve the player's current balance from PlayerPrefs (default to 100 if not set)
        playerCurrencyBalance = PlayerPrefs.GetInt("CurrencyBalance", 100);

        // Initialize life points for each BakedGoodPrefab
        foreach (var prefab in bakedGoodPrefabs)
        {
            var bakedGoodHealthComponent = prefab.GetComponent<BakedGoodHealth>();

            if (bakedGoodHealthComponent != null)
            {
                // Set initial and current life points based on the prefab's lifePoints value
                initialLifePoints.Add(bakedGoodHealthComponent.initialLifePoints);
                currentLifePoints.Add(bakedGoodHealthComponent.initialLifePoints); // Initialize current life points
                Debug.Log($"BakedGoodPrefab: {prefab.name}, Initial life points set to: {bakedGoodHealthComponent.initialLifePoints}");
            }
            else
            {
                Debug.LogError($"BakedGoodHealth component not found on {prefab.name}.");
            }
        }
    }

    // Call this method when the level is completed
    public void CompleteLevelWithPayout()
    {
        int totalPayout = 0;

        for (int i = 0; i < bakedGoodPrefabs.Count; i++)
        {
            // Get the baked good health component to access the payout
            var bakedGoodHealthComponent = bakedGoodPrefabs[i].GetComponent<BakedGoodHealth>();

            if (bakedGoodHealthComponent != null)
            {
                // Calculate payout for each BakedGood
                float lifePointsRatio = currentLifePoints[i] / initialLifePoints[i];
                int payout = CalculatePayout(lifePointsRatio, bakedGoodHealthComponent.levelPayout);

                // Update total payout
                totalPayout += payout;
                Debug.Log($"{bakedGoodPrefabs[i].name} earned {payout} currency.");
            }
        }

        // Update the player's currency balance with total payout
        playerCurrencyBalance += totalPayout;

        // Save the new balance in PlayerPrefs
        PlayerPrefs.SetInt("CurrencyBalance", playerCurrencyBalance);
        PlayerPrefs.Save();

        Debug.Log($"Level completed. Player earned total {totalPayout} currency. New balance: {playerCurrencyBalance}");
    }

    // Method to calculate the payout based on the BakedGood's life points ratio and its specific payout
    private int CalculatePayout(float lifePointsRatio, int levelPayout)
    {
        int payout = 0;

        if (lifePointsRatio == 1.0f)
        {
            payout = levelPayout;  // 100% payout
        }
        else if (lifePointsRatio >= 0.75f)
        {
            payout = (int)(levelPayout * 0.50f);  // 50% payout
        }
        else if (lifePointsRatio > 0)
        {
            payout = (int)(levelPayout * 0.20f);  // 20% payout
        }
        else
        {
            payout = 0;  // 0 payout
        }

        return payout;
    }

    // Call this method during the game to update the BakedGood's current life points
    public void UpdateCurrentLifePoints(int index, int newLifePoints)
    {
        if (index >= 0 && index < currentLifePoints.Count)
        {
            currentLifePoints[index] = newLifePoints;
            Debug.Log($"{bakedGoodPrefabs[index].name} current life points updated to: {currentLifePoints[index]}");
        }
        else
        {
            Debug.LogError("Index out of range while updating life points.");
        }
    }
}
