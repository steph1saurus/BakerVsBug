using System.Collections;

using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;

    [Header ("Public variables")]
    public int enemyCount;
    public int waveNumber = 1;
    public int maxAnts = 6;


    [Header ("Private variables")]
    //fixed spawn position for ants
    private Vector3 spawnPosition = new Vector3(-10, -1, 0);

    [Header ("Flags")]
    private bool isSpawning =false;

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount < maxAnts && !isSpawning)
        {
            //fix this. this causes start coroutine to happen continuously
            StartCoroutine(SpawnEnemy());
        }
        
    }

    IEnumerator SpawnEnemy()
    {
        isSpawning = true;
        yield return new WaitForSeconds(3f);
        Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity);
        isSpawning = false;
    }


}
