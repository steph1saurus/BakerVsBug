
using UnityEngine;
using UnityEngine.UI;

public class MoveSwatter : MonoBehaviour
{

    private Vector3 offset;
    private Collider2D swatterCollider;
    private MovePie movePie;


    private void Start()
    {
        swatterCollider = gameObject.GetComponent<Collider2D>();

        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer !=null)
        {
            spriteRenderer.sortingLayerName = "Default";
            spriteRenderer.sortingOrder = 3;
        }

        movePie = FindObjectOfType<MovePie>();


    }

    void Update()
    {


        // Move the swatter with the mouse position
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 to avoid depth issues
        transform.position = mousePosition + offset;

        //check for mouse input via Raycast
        if (Input.GetMouseButtonDown(0))
        {
            DetectSwatterClick();
        }
    }

    private void DetectSwatterClick()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0; // Keep the z-axis at 0 for 2D games

        // Perform a Raycast at the mouse position to detect the swatter
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        if (hit.collider != null && hit.collider == swatterCollider)
        {
            // Swatter was clicked, handle enemy interaction
            HandleEnemyInteraction();
        }
    }


    private void HandleEnemyInteraction()
    {
        // Find all objects with the "Enemy" tag
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            // Check if the enemy's collider is within the swatter's collider bounds
            Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

            if (enemyCollider != null && swatterCollider != null)
            {
                bool isTouching = swatterCollider.bounds.Intersects(enemyCollider.bounds);
                bool isCompletelyOverlapping = swatterCollider.bounds.Contains(enemyCollider.bounds.min) && swatterCollider.bounds.Contains(enemyCollider.bounds.max);

                if (isTouching || isCompletelyOverlapping)
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

    //private void OnMouseDown()
    //{
    //    // Find all objects with the "Enemy" tag
    //    GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

    //    offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);

    //    foreach (GameObject enemy in enemies)
    //    {
    //        // Check if the enemy's collider is within the swatter's collider bounds
    //        Collider2D enemyCollider = enemy.GetComponent<Collider2D>();

    //        if (enemyCollider != null && swatterCollider != null)
    //        {
    //            // Check if the colliders intersect (touch) or the swatter completely contains the enemy's bounds
    //            bool isTouching = swatterCollider.bounds.Intersects(enemyCollider.bounds);
    //            bool isCompletelyOverlapping = swatterCollider.bounds.Contains(enemyCollider.bounds.min) && swatterCollider.bounds.Contains(enemyCollider.bounds.max);

    //            if (isTouching || isCompletelyOverlapping)
    //            {
    //                // Reduce the enemy's life points
    //                EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
    //                if (enemyHealth != null)
    //                {
    //                    enemyHealth.ReduceLife(1); // Assuming ReduceLife(int) method reduces life points
    //                }
    //            }
    //        }
    //    }
    //}
}
