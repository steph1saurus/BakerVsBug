using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    public GameObject explosionEffect; // Reference to the explosion effect prefab

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip explosionSound;

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

        // Play the explosion sound
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound);
        }
       
        // Destroy enemies
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        yield return new WaitForSeconds(3f);
        // Destroy the bomb item
        Destroy(gameObject);
    }
}
