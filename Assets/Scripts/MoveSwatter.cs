
using UnityEngine;

public class MoveSwatter : MonoBehaviour
{

    private Vector3 offset;
    private Collider2D swatterCollider;


    private void Start()
    {
        swatterCollider = gameObject.GetComponent<Collider2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer !=null)
        {
            spriteRenderer.sortingLayerName = "Foreground";
            spriteRenderer.sortingOrder = 10;
        }

    }


    void Update()
    {
        // Move the swatter with the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 to avoid depth issues
        transform.position = mousePosition + offset;
    }

    private void OnMouseDown()
    {
        // Find all objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Check if the enemy's collider is within the swatter's collider bounds
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

            if (enemyCollider != null && swatterCollider.bounds.Intersects(enemyCollider.bounds))
            {
                // Reduce the enemy's life points
                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.ReduceLife(1); // Assuming ReduceLife(int) method reduces life points
                }
            }
        }
    }
}
