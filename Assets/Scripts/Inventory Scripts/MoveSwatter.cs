using UnityEngine;

public class MoveSwatter : MonoBehaviour
{
    private Vector3 offset;
    public float swatterZPosition = -1; // Ensure this is in front of the background
    private Camera mainCamera;
    private GameObject currentEnemy; // Store the current enemy in the trigger

    private void Start()
    {
        mainCamera = Camera.main;
        Vector3 initialPosition = transform.position;
        initialPosition.z = swatterZPosition;
        transform.position = initialPosition;
    }

    void Update()
    {
        // Move the swatter with the mouse position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = swatterZPosition; // Set z to avoid depth issues
        transform.position = mousePosition + offset;

        // Check for mouse input
        if (Input.GetMouseButtonDown(0) && currentEnemy != null)
        {
            HandleEnemyInteraction(currentEnemy);
            currentEnemy = null; // Reset the current enemy after interaction
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentEnemy = other.gameObject; // Store the enemy reference
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            currentEnemy = null; // Clear the enemy reference when it exits
        }
    }

    private void HandleEnemyInteraction(GameObject enemy)
    {
        Destroy(enemy);//update this to -1 health
    }
}
