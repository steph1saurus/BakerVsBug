using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [Header ("Inventory items")]
    
    //Swatter items
    public GameObject swatterPrefab; //reference to the prefab asset
    private GameObject instantiatedSwatter; //reference to the instantiated swatter
    public Button swatterButton;
    private bool swatterActive = false;

    //Sticky items
    public GameObject stickyPrefab;
    private GameObject instantiateSticky;
    public Button stickyButton;
    private int stickyTrapCount = 0;
    private const int maxStickyTraps = 3;


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

        //sticy button
        Button button1 = stickyButton.GetComponent<Button>();
        button1.onClick.AddListener(StickyClicked);

    }

    public void SwatterClicked()
    {
        if (!swatterActive)
        {
            ActivateSwatter();
        }
        else if (swatterActive)
        {
            DeactivatSwatter();
        }
    }

    public void ActivateSwatter()
    {
        isInventoryItemSelected = true;
        swatterActive = true;
        instantiatedSwatter = Instantiate(swatterPrefab);
    }

    public void DeactivatSwatter()
    {
        isInventoryItemSelected = false;
        swatterActive = false;

        if (instantiatedSwatter != null)
        {
            Destroy(instantiatedSwatter);
            instantiatedSwatter = null;
        }
    }

    public void StickyClicked()
    {
        if (!swatterActive)
        {
            ActivateSticky();
        }
    }

    public void ActivateSticky()
    {
        if (stickyTrapCount < maxStickyTraps) // Check if we can instantiate more sticky traps
        {
            isInventoryItemSelected = true;
            instantiateSticky = Instantiate(stickyPrefab);
            stickyTrapCount++; // Increment the counter for sticky traps
        }
      
    }

}
