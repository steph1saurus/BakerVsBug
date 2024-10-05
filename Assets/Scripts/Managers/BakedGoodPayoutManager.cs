using UnityEngine;
using System.Collections.Generic;

public class BakedGoodPayoutManager : MonoBehaviour
{
    // No need for a public list; we will fetch the objects in the CompleteLevelWithPayout method.

    // Variables to store initial and current life points for each BakedGood
    private List<float> initialLifePoints = new List<float>();
    private List<float> currentLifePoints = new List<float>();

    private void Start()
    {
        
    }

    // Call this method when the level is completed
    public void CompleteLevelWithPayout()
    {
        int totalPayout = 0;

        // Find all BakedGood GameObjects in the scene
        GameObject[] bakedGoodObjects = GameObject.FindGameObjectsWithTag("BakedGood");

        foreach (GameObject bakedGoodObject in bakedGoodObjects)
        {
            var bakedGoodHealthComponent = bakedGoodObject.GetComponent<BakedGoodHealth>();

            if (bakedGoodHealthComponent != null)
            {
                // Store the initial and current life points from the BakedGoodHealth component
                initialLifePoints.Add(bakedGoodHealthComponent.initialLifePoints);
                currentLifePoints.Add(bakedGoodHealthComponent.currentLifePoints);

                // Calculate payout based on the current life points ratio
                float lifePointsRatio = bakedGoodHealthComponent.currentLifePoints / bakedGoodHealthComponent.initialLifePoints;
                int payout = CalculatePayout(lifePointsRatio, bakedGoodHealthComponent.levelPayout);

                // Update total payout
                totalPayout += payout;
                Debug.Log($"{bakedGoodObject.name} earned {payout} currency.");
            }
            else
            {
                Debug.LogError($"BakedGoodHealth component not found on {bakedGoodObject.name}.");
            }
        }

        // Update the player's currency balance with total payout
        int playerCurrencyBalance = PlayerPrefs.GetInt("CurrencyBalance", 100); // Retrieve balance from PlayerPrefs
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
            Debug.Log($"BakedGood at index {index} current life points updated to: {currentLifePoints[index]}");
        }
        else
        {
            Debug.LogError("Index out of range while updating life points.");
        }
    }
}
