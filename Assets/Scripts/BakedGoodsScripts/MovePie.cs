using UnityEngine;

public class MovePie : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public float bakedGoodZPosition = -1f;  // Ensure BakedGood is rendered in front of enemies

    void Start()
    {
        mainCamera = Camera.main;

        // Set the z-position of the BakedGood object to ensure it's rendered in front
        Vector3 initialPosition = transform.position;
        initialPosition.z = bakedGoodZPosition;
        transform.position = initialPosition;

    }

    void OnMouseDown()
    {
     

        // Get the current mouse position and calculate the offset
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, bakedGoodZPosition);
    }

    void OnMouseDrag()
    {
        // Get the current mouse position and update the object's position, maintaining the correct z-position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, bakedGoodZPosition) + offset;

        // Update the object's position to follow the mouse, maintaining the z-layer priority
        transform.position = newPosition;
    }
}
