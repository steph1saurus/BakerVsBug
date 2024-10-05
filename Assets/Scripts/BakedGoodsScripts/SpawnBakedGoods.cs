using System.Collections;
using UnityEngine;

public class SpawnBakedGoods : MonoBehaviour
{
    public GameObject[] bakedGoodsPrefab;
    public float spawnRadius = 5f;
    public LayerMask bakedGoodsLayerMask;
    public LayerMask enemyLayerMask;
    private Vector3 spawnPosition;
    private Camera mainCamera;

    public float maxYSpawnHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main; // Get the main camera reference

        spawnPosition = new Vector3(0, 0, 0);
        Instantiate(bakedGoodsPrefab[0], spawnPosition, Quaternion.identity);
        StartCoroutine(spawnNewBakedGoods());
    }

    IEnumerator spawnNewBakedGoods()
    {
        while (true)
        {
            yield return new WaitForSeconds(20f);

            Vector3 newSpawnPosition = GetValidSpawnPosition();
            if (newSpawnPosition != Vector3.zero)
            {
                GameObject newBakedGood = Instantiate(bakedGoodsPrefab[Random.Range(0, bakedGoodsPrefab.Length)],
                    newSpawnPosition, Quaternion.identity);

                // Start the coroutine to check for enemy collision and apply damage after 3 seconds
                StartCoroutine(CheckForEnemyCollision(newBakedGood));
            }
        }
    }

    Vector3 GetValidSpawnPosition()
    {
        for (int i = 0; i < 10; i++) // Try 10 times to find a valid position
        {
            Vector3 potentialPosition = GetRandomScreenPosition();

            // Check if this position overlaps with any existing baked goods
            Collider2D[] colliders = Physics2D.OverlapCircleAll(potentialPosition, spawnRadius, bakedGoodsLayerMask);
            if (colliders.Length == 0) // If no overlap, it's a valid position
            {
                return potentialPosition;
            }
        }
        return Vector3.zero;
    }

    Vector3 GetRandomScreenPosition()
    {
        // Get the screen bounds in world space
        Vector3 screenBottomLeft = mainCamera.ViewportToWorldPoint(new Vector3(0, 0, mainCamera.nearClipPlane));
        Vector3 screenTopRight = mainCamera.ViewportToWorldPoint(new Vector3(1, 1, mainCamera.nearClipPlane));

        // Generate a random position within the screen bounds
        float randomX = Random.Range(screenBottomLeft.x, screenTopRight.x);
        float randomY = Random.Range(screenBottomLeft.y, Mathf.Min(screenTopRight.y, maxYSpawnHeight)); // Limit the y position

        return new Vector3(randomX, randomY, 0); // Set Z to 0 for 2D
    }

    IEnumerator CheckForEnemyCollision(GameObject bakedGood)
    {
        Collider2D bakedGoodCollider = bakedGood.GetComponent<Collider2D>();

        // Check for collision with enemies
        Collider2D[] enemyColliders = Physics2D.OverlapCircleAll(bakedGood.transform.position, spawnRadius, enemyLayerMask);

        if (enemyColliders.Length > 0) // If overlapping with an enemy
        {
            yield return new WaitForSeconds(3f); // Wait for 3 seconds

            // Apply damage or other effects to the baked good
            Debug.Log("Baked good overlapped with enemy and took damage!");
        }
    }
}
