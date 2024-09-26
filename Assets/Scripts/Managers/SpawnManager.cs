
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab; // Assuming enemyPrefab[0] is Ant and enemyPrefab[1] is BigAnt

    [Header("Spawn wave")]
    public int enemyCount;
    public int waveNumber = 1;
    public int maxEnemies = 20;


   

    [Header("Spawn area")]
    
    public Collider2D backgroundCollider;

    private void Start()
    {
        SpawnEnemyWave(waveNumber);
    }

    void Update()
    {
       
        enemyCount = FindObjectsOfType<EnemyHealth>().Length; //length will return the number of enemies in the scene
        if (enemyCount == 0)
        {
            if (waveNumber <= maxEnemies)
            {
                waveNumber++;
                SpawnEnemyWave(waveNumber);

            }

        }
      

    }

    // Spawn a wave of enemies based on the wave number
    void SpawnEnemyWave(int enemiesToSpawn)
    {
            if (waveNumber <8)
            {
                // Spawn only ants for wave 1
                for (int i = 0; i < enemiesToSpawn; i++)
                {
                    Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                    Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity); // Ant prefab
                }
            }
            else
            {
            int enemyIndex = Random.Range(0, 1);

            for (int i = 0; i < enemiesToSpawn; i++)
            {
                Vector3 spawnPosition = GetRandomPositionOutsideBounds();
                Instantiate(enemyPrefab[enemyIndex], spawnPosition, Quaternion.identity); // Ant prefab
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
