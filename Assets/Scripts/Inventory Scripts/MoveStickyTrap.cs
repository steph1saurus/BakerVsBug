using UnityEngine;
using System.Collections;

public class MoveStickyTrap : MonoBehaviour
{
    private Vector3 offset;
    private Camera mainCamera;
    public float stickyZPos = 0f;

    void Start()
    {
        mainCamera = Camera.main;

        // Set the z-position of the object to ensure it's rendered in front
        Vector3 initialPosition = transform.position;
        initialPosition.z = stickyZPos;
        transform.position = initialPosition;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {


        if (other.CompareTag("Enemy"))
        {
            AntController antController = other.GetComponent<AntController>(); // Assuming the enemy has an EnemyMovement script
            if (antController != null)
            {
                StartCoroutine(SlowDownEnemy(antController));
            }
        }
    }

    void OnMouseDown()
    {
        // Get the current mouse position and calculate the offset
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        offset = transform.position - new Vector3(mousePosition.x, mousePosition.y, stickyZPos);
    }

    void OnMouseDrag()
    {
        // Get the current mouse position and update the object's position, maintaining the correct z-position
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        Vector3 newPosition = new Vector3(mousePosition.x, mousePosition.y, stickyZPos) + offset;

        // Update the object's position to follow the mouse, maintaining the z-layer priority
        transform.position = newPosition;
    }

    private IEnumerator SlowDownEnemy(AntController antController)
    {
        float originalSpeed = antController.speed; // Store the original speed
        antController.speed *= 0.2f; // Slow down the enemy to 1/5 of its speed

        // Wait for 3 seconds
        yield return new WaitForSeconds(5f);

        antController.speed = originalSpeed; // Restore the original speed
    }
}
