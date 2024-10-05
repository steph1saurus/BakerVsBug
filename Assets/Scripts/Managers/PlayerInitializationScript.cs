using UnityEngine;

public class PlayerInitializationScript : MonoBehaviour
{
    private void Start()
    {
     

        InitializePlayerData();
    }

    // Method to initialize player data if not already set
    void InitializePlayerData()
    {
        
        if (!PlayerPrefs.HasKey("CurrencyBalance"))
        {
            // Set initial currency balance
            PlayerPrefs.SetInt("CurrencyBalance", 100);
            Debug.Log("Initial currency balance set to 100");

            // Set initial inventory
            InitializeInventory();

            // Save the changes
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Player data already initialized.");
        }
    }

    // Method to initialize player's inventory
    void InitializeInventory()
    {
        int[] inventoryIDs = { 0, 1, 2, 3, 4, 5 }; // Inventory IDs
        int[] initialQuantities = { 1, 3, 2, 1, 1,1 }; // Initial quantities

        for (int i = 0; i < inventoryIDs.Length; i++)
        {
            PlayerPrefs.SetInt($"Inventory_{inventoryIDs[i]}", initialQuantities[i]);
            Debug.Log($"Initial quantity for inventory ID {inventoryIDs[i]} set to {initialQuantities[i]}");
        }
    }

    // Method to retrieve the player's current balance
    public int GetCurrencyBalance()
    {
        return PlayerPrefs.GetInt("CurrencyBalance", 100);
    }

    // Method to retrieve the player's inventory quantity by ID
    public int GetInventoryQuantity(int inventoryID)
    {
        return PlayerPrefs.GetInt($"Inventory_{inventoryID}", 0); // Default to 0 if not found
    }
}
