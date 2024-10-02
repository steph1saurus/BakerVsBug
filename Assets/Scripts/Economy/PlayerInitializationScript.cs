using UnityEngine;

public class PlayerInitializationScript : MonoBehaviour
{
    // Prefab references
    public GameObject swatterPrefab;
    public GameObject stickytrapPrefab;
    public GameObject sprayPrefab;
    public GameObject bigSwatterPrefab;
    public GameObject sugartrapPrefab;
    public GameObject BugbombPrefab;

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
            PlayerPrefs.SetString("Inventory", "swatterPrefab,stickytrapPrefab");
            Debug.Log("Initial inventory set with swatterPrefab and stickytrapPrefab");

            // Set initial completed level
            PlayerPrefs.SetInt("CompletedLevel", 1);
            Debug.Log("Initial completed level set to 1");

            // Set unlockable items as empty (none are unlocked yet)
            PlayerPrefs.SetString("UnlockableItems", "");

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

            // Unlock new items for purchase based on the level completed
            UnlockNewItemsForPurchase(levelCompleted);

            // Save changes
            PlayerPrefs.Save();
        }
    }

    // Method to unlock new items for purchase based on completed levels
    void UnlockNewItemsForPurchase(int levelCompleted)
    {
        string unlockedItems = PlayerPrefs.GetString("UnlockableItems", "");

        if (levelCompleted >= 2)
        {
            if(!unlockedItems.Contains("sprayPrefab"))
            {
                unlockedItems += ",sprayPrefab";

            }

            if (!unlockedItems.Contains("bigSwatterPrefab"))
            {
                unlockedItems += ",bigSwatterPrefab";
            }
            if (!unlockedItems.Contains("sugartrapPrefab"))
            {
                unlockedItems += ",sugartrapPrefab";
            }
            if (!unlockedItems.Contains("bugbombPrefab"))
            {
                unlockedItems += ",bugbombPrefab";
            }

        }
        PlayerPrefs.SetString("UnlockableItems", unlockedItems);
        PlayerPrefs.Save();
    }

    // Method to allow the player to purchase an item
    public bool PurchaseItem(string itemName)
    {
        string unlockableItems = PlayerPrefs.GetString("UnlockableItems", "");
        string currentInventory = PlayerPrefs.GetString("Inventory", "");
        int currentBalance = PlayerPrefs.GetInt("CurrencyBalance", 100);

        int itemCost = GetItemCost(itemName); // Determine the cost of the item

        // Check if the item is unlocked and if the player has enough currency
        if (unlockableItems.Contains(itemName) && currentBalance >= itemCost)
        {
            // Deduct the cost from player's currency
            currentBalance -= itemCost;
            PlayerPrefs.SetInt("CurrencyBalance", currentBalance);

            // Add the item to the player's inventory
            currentInventory += "," + itemName;
            PlayerPrefs.SetString("Inventory", currentInventory);

            PlayerPrefs.Save();

            Debug.Log(itemName + " purchased for " + itemCost + " currency.");
            return true;
        }
        else
        {
            Debug.Log("Cannot purchase " + itemName + ". Either it's not unlocked or insufficient funds.");
            return false;
        }
    }

    // Method to get the cost of an item (you can modify the prices here)
    private int GetItemCost(string itemName)
    {
        switch (itemName)
        {
            case "sprayPrefab":
                return 50; // Cost for netTrapPrefab
            case "bigSwatterPrefab":
                return 50; // Cost for swatterPrefab
            case "sugartrapPrefab":
                return 50; // Cost for stickytrapPrefab
            case "bugbombPrefab":
                return 200; // Cost for stickytrapPrefab
            default:
                return 0; // Unknown item cost
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

    // Method to retrieve unlockable items
    public string[] GetUnlockableItems()
    {
        string unlockableItems = PlayerPrefs.GetString("UnlockableItems", "");
        return unlockableItems.Split(',');
    }
}
