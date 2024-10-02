using System.Collections;

using UnityEngine;

public class SpawnBakedGoods : MonoBehaviour
{
    public GameObject[] bakedGoodsPrefab;
    public float spawnRadius = 5f;
    public LayerMask bakedGoodsLayerMask;
    public LayerMask enemyLayerMask;
    private Vector3 spawnPosition;



    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        Instantiate(bakedGoodsPrefab[0], spawnPosition, Quaternion.identity);
        StartCoroutine(spawnNewBakedGoods());

    }

    // Update is called once per frame
    void Update()
    {
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
            Vector3 potentialPosition = new Vector3(
                Random.Range(-10f, 10f), // Adjust the range as per your scene
                Random.Range(-10f, 10f),
                0
            );

            // Check if this position overlaps with any existing baked goods
            Collider2D[] colliders = Physics2D.OverlapCircleAll(potentialPosition, spawnRadius, bakedGoodsLayerMask);
            if (colliders.Length == 0) // If no overlap, it's a valid position
            {
                return potentialPosition;
            }
        }
        return Vector3.zero;
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
            // (You can add your custom logic here)
            Debug.Log("Baked good overlapped with enemy and took damage!");

        }
    }
}

