using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    //public GameObject[] inventoryPrefab;

    public int enemyCount;
    public int waveNumber = 1;
    public int maxAnts = 3;

    //fixed spawn position for ants
    private Vector3 spawnPosition = new Vector3(-10, -1, 0);

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;

        if (enemyCount < maxAnts)
        {
            //fix this. this causes start coroutine to happen continuously
            StartCoroutine(SpawnEnemy());
        }
        
    }

    IEnumerator SpawnEnemy()
    {
        yield return new WaitForSeconds(2f);
        Instantiate(enemyPrefab[0], spawnPosition, Quaternion.identity);
    }


}
