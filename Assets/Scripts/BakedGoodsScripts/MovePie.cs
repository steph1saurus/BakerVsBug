
using UnityEngine;

public class MovePie : MonoBehaviour
{
    public bool moving = false;
    private Vector3 offset;

    private InventoryManager inventoryManager;
 


    private void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.sortingOrder = 2;
        }
        inventoryManager = FindObjectOfType<InventoryManager>();
    }
    // Update is called once per frame
    void Update()
    {
        // Check if an inventory item is selected before moving the pie
        if (!inventoryManager.isInventoryItemSelected && moving)
        {
            // Move the pie, taking into account the original offset
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; // Set z to 0 to avoid depth issues
            transform.position = mousePosition + offset;
        }

        //if (moving)
        //{
        //    //move the pie, taking into account the original offset
        //    transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        //}
    }

    private void OnMouseDown()
    {

        if (!inventoryManager.isInventoryItemSelected)
        {
            // Toggle the moving state
            moving = !moving;
        }
        else if (!inventoryManager.isInventoryItemSelected && !moving)
        {
            moving = true;
        }
       
        //{
        //    moving = true;
        //    //record the difference between the centers of the pie and the clicked point on the camera plane
        //    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //}

        //else moving = false;
    }
}
