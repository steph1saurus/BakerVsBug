using System.Collections;
using UnityEngine;
using TMPro;


public class SpawnManager : MonoBehaviour
{
    
    [Header("Game objects")]
    [SerializeField] public GameObject[] enemyPrefab; // Assuming enemyPrefab[0] is Ant and enemyPrefab[1] is BigAnt

    [Header("Spawn wave")]
    [SerializeField] public int enemyCount;
    [SerializeField] public int waveNumber = 1;
    [SerializeField] public int maxEnemies;
    
    [Header("Spawn area")]

    [SerializeField] public Collider2D backgroundCollider;

    [Header("Level Number")]
    [SerializeField] int levelNum;

    private void Start()
    {
        SpawnEnemyWave(waveNumber + 2);
    }


    void Update()
    {

        enemyCount = FindObjectsOfType<EnemyHealth>().Length; //length will return the number of enemies in the scene

        if (enemyCount == 0)
        {
            if (waveNumber <= maxEnemies)
            {
                waveNumber++;
    
                    SpawnEnemyWave(waveNumber + 2);
            
            }

        }

    }

    // Spawn a wave of enemies based on the wave number
    void SpawnEnemyWave(int enemiesToSpawn)
    {
      
        if (waveNumber < 4)
        {
            // Spawn only ants for wave 1
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity); 
            }
        }
                


        else if (waveNumber >= 4 && waveNumber < 6)

        {
            // For waves 4 and above, ensure no more than one enemyPrefab[3] and one enemyPrefab[4] are spawned
            bool spawnedEnemy3 = false; // Track if enemyPrefab[3] is spawned
            bool spawnedEnemy4 = false; // Track if enemyPrefab[4] is spawned

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                int randomIndex;

                // Ensure enemyPrefab[3] and enemyPrefab[4] are only spawned once per wave
                if (!spawnedEnemy3 && !spawnedEnemy4)
                {
                    // Randomly select one of enemyPrefab[3] or enemyPrefab[4] if neither has been spawned
                    randomIndex = Random.Range(0, 5); // Range is 0 to 4 to include both prefabs
                    if (randomIndex == 3)
                    {
                        spawnedEnemy3 = true;
                    }
                    else if (randomIndex == 4)
                    {
                        spawnedEnemy4 = true;
                    }
                }
                else if (!spawnedEnemy3)
                {
                    // Only allow enemyPrefab[3] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 4); // Exclude prefab[4] from the random range
                    if (randomIndex == 3)
                    {
                        spawnedEnemy3 = true;
                    }
                }
                else if (!spawnedEnemy4)
                {
                    // Only allow enemyPrefab[4] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 4); // Exclude prefab[3] from the random range
                    if (randomIndex == 4)
                    {
                        spawnedEnemy4 = true;
                    }
                }
                else
                {
                    // If both enemyPrefab[3] and enemyPrefab[4] have spawned, pick from the first three prefabs
                    randomIndex = Random.Range(0, 3); // Only allow prefabs 0 to 2 to spawn
                }

                // Spawn the selected enemy
                Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
            }
        }
        else if (waveNumber >= 6)
        {
            // For waves 4 and above, ensure no more than one enemyPrefab[3], [4], [5], or [6] are spawned
            bool spawnedEnemy3 = false;
            bool spawnedEnemy4 = false;
            bool spawnedEnemy5 = false;
            bool spawnedEnemy6 = false;

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                int randomIndex;

                // Handle the spawning of enemyPrefab[3], [4], [5], and [6]
                if (!spawnedEnemy3 && !spawnedEnemy4 && !spawnedEnemy5 && !spawnedEnemy6)
                {
                    // Randomly select one of enemyPrefab[3], [4], [5], or [6] if none has been spawned
                    randomIndex = Random.Range(0, 7); // Range includes 0 to 6
                    if (randomIndex == 3) spawnedEnemy3 = true;
                    else if (randomIndex == 4) spawnedEnemy4 = true;
                    else if (randomIndex == 5) spawnedEnemy5 = true;
                    else if (randomIndex == 6) spawnedEnemy6 = true;
                }
                else if (!spawnedEnemy3)
                {
                    // Only allow enemyPrefab[3] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 6); // Exclude prefab[6]
                    if (randomIndex == 3) spawnedEnemy3 = true;
                }
                else if (!spawnedEnemy4)
                {
                    // Only allow enemyPrefab[4] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 6); // Exclude prefab[6]
                    if (randomIndex == 4) spawnedEnemy4 = true;
                }
                else if (!spawnedEnemy5)
                {
                    // Only allow enemyPrefab[5] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 6); // Exclude prefab[6]
                    if (randomIndex == 5) spawnedEnemy5 = true;
                }
                else if (!spawnedEnemy6)
                {
                    // Only allow enemyPrefab[6] to spawn if it hasn't spawned yet
                    randomIndex = Random.Range(0, 6);
                    if (randomIndex == 6) spawnedEnemy6 = true;
                }
                else
                {
                    // If all special enemies have spawned, pick from the first 3 prefabs (0 to 2)
                    randomIndex = Random.Range(0, 3);
                }

                // Spawn the selected enemy
                Instantiate(enemyPrefab[randomIndex], spawnPosition, Quaternion.identity);
            }
        }
        
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
                randomX = Random.Range(bounds.min.x, bounds.max.x);
                randomY = bounds.max.y;
                break;
            case 1: // Bottom
                randomX = Random.Range(bounds.min.x, bounds.max.x);
                randomY = bounds.min.y;
                break;
            case 2: // Left
                randomX = bounds.min.x;
                randomY = Random.Range(bounds.min.y, bounds.max.y);
                break;
            case 3: // Right
                randomX = bounds.max.x;
                randomY = Random.Range(bounds.min.y, bounds.max.y);
                break;
            default:
                randomX = bounds.max.x;
                randomY = Random.Range(bounds.min.y, bounds.max.y);
                break;
        }

        return new Vector3(randomX, randomY, 0f); // Assuming a 2D game (z = 0)
    }

}
