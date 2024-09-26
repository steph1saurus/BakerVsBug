
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header ("Inventory items")]
    public GameObject swatterPrefab; //reference to the prefab asset
    private GameObject instantiatedSwatter; //reference to the instantiated swatter

    [Header("Inventory UI")]
    public GameObject inventoryPanel;
    private bool inventoryActive= true;


    [Header("Variables")]
    public bool swatterSelected = false;


    

    public void OnMouseDown()
    {
        if (!swatterSelected)
        {
            ActivateSwatter();
        }
        else if (swatterSelected)
        {
            DeactivatSwatter();
        }
    }


    void ActivateSwatter()
    {
        swatterSelected = true;
        instantiatedSwatter = GameObject.Instantiate(swatterPrefab);
    }

    void DeactivatSwatter()
    {
        swatterSelected = false;

        if (instantiatedSwatter != null)
        {
            Destroy(instantiatedSwatter);
        }
    }

    public void ActivateInventory()
    {
        if (!inventoryActive)
        {
            inventoryPanel.SetActive(true);
            inventoryActive = true;
        }
        else
        {
            inventoryPanel.SetActive(false);
            inventoryActive = false;
        }
    }
       

   
}
