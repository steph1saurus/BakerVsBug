using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;


public class TestInventoryManager : MonoBehaviour
{
    [System.Serializable]
    public class InventoryItem
    {
        public string itemName;
        public GameObject prefab; // The prefab to instantiate
        public Sprite buttonImage; // The image to display on the button
    }

    // List of inventory items
    public List<InventoryItem> inventoryItems;

    // UI Button prefab for instantiation
    public Button buttonPrefab;

    // Parent object to hold the buttons
    public Transform buttonParent;

    public int maxButtons = 4;

    // Start is called before the first frame update
    void Start()
    {
        PopulateInventoryButtons();
    }

    // Populate buttons based on player's inventory
    private void PopulateInventoryButtons()
    {
        // Clear existing buttons
        foreach (Transform child in buttonParent)
        {
            Destroy(child.gameObject);
        }

        // Create a count to track the number of buttons created
        int buttonCount = 0;

        // Loop through the inventory items and create buttons
        foreach (var item in inventoryItems)
        {
            if (PlayerPrefs.GetInt(item.itemName, 0) > 0) // Check if the player has this item
            {
                if (buttonCount >= maxButtons) // Check if we reached the max button limit
                    break;

                Button newButton = Instantiate(buttonPrefab, buttonParent);
                newButton.GetComponent<Image>().sprite = item.buttonImage;
                newButton.GetComponentInChildren<Text>().text = item.itemName;

                // Add listener to the button to instantiate the prefab when clicked
                newButton.onClick.AddListener(() => InstantiatePrefab(item.prefab, buttonCount));

                buttonCount++; // Increment the button count
            }
        }
    }

    // Method to instantiate the prefab
    private void InstantiatePrefab(GameObject prefab, int index)
    {
        Vector3 position = Vector3.zero;
        // Set position based on the button index
        switch (index)
        {
            case 0:
                position = new Vector3(-494, 0, 0);
                break;
            case 1:
                position = new Vector3(-310, 0, 0);
                break;
            case 2:
                position = new Vector3(-128, 0, 0);
                break;
            case 3:
                position = new Vector3(60, 0, 0);
                break;
        }
        // Instantiate the prefab at a specified location, e.g., at the origin or the player's position
        Instantiate(prefab, position, Quaternion.identity);
        Debug.Log($"{prefab.name} instantiated.");
    }

    // Method to refresh the inventory buttons (if needed, e.g., after adding new items)
    public void RefreshInventoryButtons()
    {
        PopulateInventoryButtons();
    }
}
