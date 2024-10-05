using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyItem : MonoBehaviour
{
    public int ID;
    private LevelEditorManager levelEditorManager;
    [SerializeField] SoundMixerManager soundMixerManager;

    // Store enemies that are inside the sticky trap
    private List<EnemyHealth> stuckEnemies = new List<EnemyHealth>();

    // Max number of enemies that can be stuck
    public int maxStuckEnemies = 3;

    // Time in seconds before stuck enemies are destroyed
    public float destroyDelay = 3f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip stickySound;

    private Coroutine soundCoroutine; // Coroutine reference for sound playback

    void Start()
    {
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        stickySound = soundMixerManager.stickySound;
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            // Destroy the sticky trap and restore the speed of any trapped enemies
            foreach (EnemyHealth enemyHealth in stuckEnemies)
            {
                if (enemyHealth != null)
                {
                    enemyHealth.GetComponent<EnemyController>().speed = enemyHealth.GetComponent<EnemyController>().initialSpeed; // Reset to the enemy's initial speed
                }
            }

            Destroy(gameObject);
            levelEditorManager.itemButtons[ID].quantity++;
            levelEditorManager.itemButtons[ID].quantityTxt.text = levelEditorManager.itemButtons[ID].quantity.ToString();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();

            // Only stick enemies with lifePoints <= 3 and if fewer than 3 enemies are stuck
            if (enemyHealth != null && enemyHealth.lifePoints <= 3 && stuckEnemies.Count < maxStuckEnemies)
            {
                // Only add the enemy if it's not already in the list
                if (!stuckEnemies.Contains(enemyHealth))
                {
                    stuckEnemies.Add(enemyHealth);
                    other.GetComponent<EnemyController>().speed = 0; // Set the enemy's speed to 0

                    // Start coroutine to destroy the enemy after a delay
                    StartCoroutine(DestroyEnemyAfterDelay(enemyHealth));

                    // Start playing the sticky sound in a loop
                    if (soundCoroutine == null)
                    {
                        soundCoroutine = StartCoroutine(PlayStickySound());
                    }
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = other.GetComponent<EnemyHealth>();
            if (stuckEnemies.Contains(enemyHealth))
            {
                stuckEnemies.Remove(enemyHealth); // Remove enemy from the list if they exit the sticky trap

                // Stop playing sound if no enemies are left
                if (stuckEnemies.Count == 0 && soundCoroutine != null)
                {
                    StopCoroutine(soundCoroutine);
                    soundCoroutine = null; // Reset the reference
                }
            }
        }
    }

    // Coroutine to destroy the enemy after a delay
    private IEnumerator DestroyEnemyAfterDelay(EnemyHealth enemyHealth)
    {
        yield return new WaitForSeconds(destroyDelay);

        // Ensure the enemy is still stuck and not destroyed already
        if (stuckEnemies.Contains(enemyHealth))
        {
            stuckEnemies.Remove(enemyHealth);
            Destroy(enemyHealth.gameObject);
        }
    }

    // Coroutine to play sticky sound every second while enemies are stuck
    private IEnumerator PlayStickySound()
    {
        while (stuckEnemies.Count > 0)
        {
            if (audioSource != null && stickySound != null)
            {
                audioSource.PlayOneShot(stickySound);
            }
            yield return new WaitForSeconds(1f); // Wait for 1 second before playing the sound again
        }
        soundCoroutine = null; // Reset the reference when finished
    }
}
