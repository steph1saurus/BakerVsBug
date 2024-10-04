using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    public GameObject explosionEffect; // Reference to the explosion effect prefab

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CountDownToExplode());
    }

    private IEnumerator CountDownToExplode()
    {

        // Instantiate explosion effect
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(5f);

        // Destroy enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }

        // Destroy the bomb item
        Destroy(gameObject);
    }
}
