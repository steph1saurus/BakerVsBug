using System.Collections;
using UnityEngine;

public class BombItem : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField]public int ID;

    [Header("Explosion particles")]
    [SerializeField] public GameObject explosionEffect; // Reference to the explosion effect prefab

    [Header("Audio")]
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip explosionSound;

    [SerializeField] SoundMixerManager soundMixerManager;

    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        explosionSound = soundMixerManager.explosionSound;
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
