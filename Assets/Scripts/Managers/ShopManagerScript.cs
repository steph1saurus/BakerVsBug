using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5, 7]; // Changed array size to accommodate 6 items
    public int coins; // Player's currency

    public TextMeshProUGUI coinsTxt;

    // Start is called before the first frame update
    void Start()
    {
        // Retrieve the player's current coins from PlayerPrefs
        coins = PlayerPrefs.GetInt("CurrencyBalance");
        coinsTxt.text = "Wallet: " + coins.ToString();

        // Initialize shop items array
        shopItems[1, 1] = 0; // swatter
        shopItems[1, 2] = 1; // sticky
        shopItems[1, 3] = 2; // sugar
        shopItems[1, 4] = 3; // big swatter
        shopItems[1, 5] = 4; // bomb
        shopItems[1, 6] = 5; // spray

        // Prices
        shopItems[2, 0] = 5;
        shopItems[2, 1] = 50;
        shopItems[2, 2] = 30;
        shopItems[2, 3] = 10;
        shopItems[2, 4] = 50;
        shopItems[2, 5] = 100;

        // Quantity
        shopItems[3, 0] = 1;
        shopItems[3, 1] = 2;
        shopItems[3, 2] = 2;
        shopItems[3, 3] = 1;
        shopItems[3, 4] = 1;
        shopItems[3, 5] = 1;
    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        // Check if the player has enough coins
        int itemID = buttonRef.GetComponent<ButtonInfo>().itemID;
        int itemPrice = shopItems[2, itemID];

        if (coins >= itemPrice)
        {
            // Subtract the item price from the player's coins
            coins -= itemPrice;

            // Retrieve the current quantity from PlayerPrefs for this item
            int currentQuantity = PlayerPrefs.GetInt($"Inventory_{itemID}");

            // Update the shopItems array with the new quantity (adding the current quantity)
            shopItems[3, itemID] = currentQuantity + 1;

            // Update wallet display
            coinsTxt.text = "Wallet: " + coins.ToString();
            buttonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[3, itemID].ToString(); // Update item quantity text

            // Save the updated coins value to PlayerPrefs
            PlayerPrefs.SetInt("CurrencyBalance", coins);

            // Save the updated quantity to PlayerPrefs
            PlayerPrefs.SetInt($"Inventory_{itemID}", shopItems[3, itemID]);

            // Save the changes to PlayerPrefs
            PlayerPrefs.Save();

            Debug.Log($"Bought item {itemID} for {itemPrice} coins. New balance: {coins}, New quantity: {shopItems[3, itemID]}");
        }
        else
        {
            Debug.Log("Not enough coins to purchase the item.");
        }
    }


}
