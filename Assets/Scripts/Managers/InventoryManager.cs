using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header ("Inventory items")]
    
    //Swatter items
    public GameObject swatterPrefab; //reference to the prefab asset
    private GameObject instantiatedSwatter; //reference to the instantiated swatter
    public Button swatterButton;


    [Header("Variables")]
    public bool isInventoryItemSelected = false;

    //Sticky trap variables
    public float speedReductionFactor = 0.5f;
    public float effectDuration = 5f;

    private void Start()
    {
        //swatterbutton
        Button button = swatterButton.GetComponent<Button>();
        button.onClick.AddListener(SwatterClicked);

    }

    public void SwatterClicked()
    {
        if (!isInventoryItemSelected)
        {
            ActivateSwatter();
        }
        else if (isInventoryItemSelected)
        {
            DeactivatSwatter();
        }
    }

    public void ActivateSwatter()
    {
        isInventoryItemSelected = true;
        instantiatedSwatter = Instantiate(swatterPrefab);
    }

    public void DeactivatSwatter()
    {
        isInventoryItemSelected = false;

        if (instantiatedSwatter != null)
        {
            Destroy(instantiatedSwatter);
            instantiatedSwatter = null;
        }
    }


    
}
