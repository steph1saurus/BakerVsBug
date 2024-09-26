using System.Collections;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    [Header ("Variables")]
    public int enemyCount;
    public int waveNumber = 0;
    public int maxAnts = 20;
    public int maxBigAnts = 10;
    public float waveCoolDownTime = 10f;
    public float timeSinceLastWave = 0f;

    public Collider2D backgroundCollider;
    public Vector2 spawnPadding = new Vector2(2, 2); // Padding outside the background

   

    [Header("Flags")]
    private bool isSpawning = false;


    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        timeSinceLastWave += Time.deltaTime;

        if ((enemyCount >0 && enemyCount < maxAnts && !isSpawning) || timeSinceLastWave >= waveCoolDownTime)
        {
            //fix this. this causes start coroutine to happen continuously
            StartCoroutine(SpawnEnemyWave(waveNumber));
            timeSinceLastWave = 0f;
        }
        else if (enemyCount ==0 )
        {
            waveNumber ++;
            timeSinceLastWave = 0f;
        }
    }

    //IEnumerator SpawnEnemy()
    //{
    //    isSpawning = true;

    //    yield return new WaitForSeconds(1f); // Shortened wait time for this example

    //    // Choose a random position outside the background collider
    //    Vector3 spawnPosition = GetRandomPositionOutsideBounds();

    //    // Instantiate an ant at the chosen position
    //    Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity);

    //    isSpawning = false;
    //}

    IEnumerator SpawnEnemyWave(int waveNumber)
    {
        isSpawning = true;
        yield return new WaitForSeconds(2f); // Time between wave spawns

        if (waveNumber == 1)
        {
            // Spawn only ants for wave 1
            for (int i = 0; i < maxAnts; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity); // Ant prefab
            }
        }
        else if (waveNumber >= 2)
        {
            // Spawn ants and big ants for wave 2 and onwards
            for (int i = 0; i < maxAnts; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity); // Ant prefab
            }

            for (int i = 0; i < maxBigAnts; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                Instantiate(enemyPrefab[1], spawnPosition, Quaternion.identity); // BigAnt prefab
            }
        }

        isSpawning = false;
    }

    // Get a random position outside the edges of the background collider
    Vector3 GetRandomPositionOutsideBounds()
    {
        Bounds bounds = backgroundCollider.bounds;

        // Get random spawn position outside the background collider bounds
        float randomX, randomY;

        int side = Random.Range(0, 4); // 0 = top, 1 = bottom, 2 = left, 3 = right
        switch (side)
        {
            case 0: // Top
                randomX = Random.Range(bounds.min.x - spawnPadding.x, bounds.max.x + spawnPadding.x);
                randomY = bounds.max.y + spawnPadding.y;
                break;
            case 1: // Bottom
                randomX = Random.Range(bounds.min.x - spawnPadding.x, bounds.max.x + spawnPadding.x);
                randomY = bounds.min.y - spawnPadding.y;
                break;
            case 2: // Left
                randomX = bounds.min.x - spawnPadding.x;
                randomY = Random.Range(bounds.min.y - spawnPadding.y, bounds.max.y + spawnPadding.y);
                break;
            case 3: // Right
                randomX = bounds.max.x + spawnPadding.x;
                randomY = Random.Range(bounds.min.y - spawnPadding.y, bounds.max.y + spawnPadding.y);
                break;
            default:
                randomX = bounds.max.x + spawnPadding.x;
                randomY = Random.Range(bounds.min.y - spawnPadding.y, bounds.max.y + spawnPadding.y);
                break;
        }

        return new Vector3(randomX, randomY, 0f); // Assuming a 2D game (z = 0)
    }

}
