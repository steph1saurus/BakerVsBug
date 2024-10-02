using UnityEngine;

public class PlayerInitializationScript : MonoBehaviour

{  private void Start()
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
            PlayerPrefs.SetString("Inventory", "swatterPrefab,stickytrapPrefab");
            Debug.Log("Initial inventory set with swatterPrefab and stickytrapPrefab");

            // Set initial completed level
            PlayerPrefs.SetInt("CompletedLevel", 0);
            Debug.Log("Initial completed level set to 0");

            
            // Save the changes
            PlayerPrefs.Save();
        }
        else
        {
            Debug.Log("Player data already initialized.");
        }
    }

    // Method to update player progress after completing a level
    public void CompleteLevel(int levelCompleted)
    {
        int currentCompletedLevel = PlayerPrefs.GetInt("CompletedLevel", 1);

        if (levelCompleted > currentCompletedLevel)
        {
            PlayerPrefs.SetInt("CompletedLevel", levelCompleted);
            Debug.Log("Player completed level " + levelCompleted);

            
            // Save changes
            PlayerPrefs.Save();
        }
    }

    // Method to retrieve the player's current balance
    public int GetCurrencyBalance()
    {
        return PlayerPrefs.GetInt("CurrencyBalance", 100);
    }

    // Method to retrieve the player's inventory
    public string[] GetInventory()
    {
        string inventoryString = PlayerPrefs.GetString("Inventory", "swatterPrefab,stickytrapPrefab");
        return inventoryString.Split(',');
    }

    
}
