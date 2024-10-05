using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatterItem : MonoBehaviour
{
    public int ID;
    private SoundMixerManager soundMixerManager;
    private LevelEditorManager levelEditorManager;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip swatterSound;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();

        swatterSound = soundMixerManager.swatterSound;
    }

    // Update is called once per frame
    void Update()
    {
        // Update swatter position to follow the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we are in 2D
        transform.position = mousePosition;

        // Handle mouse button inputs
        if (Input.GetMouseButtonDown(0))
        {
            // Check for enemies within the trigger area
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust radius as needed

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    // Reduce enemy health by 1
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.lifePoints -= 1;

                        // Play swatter sound when enemy health is reduced
                        if (audioSource != null && swatterSound != null)
                        {
                            audioSource.PlayOneShot(swatterSound);
                        }


                        // Optionally destroy the enemy if health reaches 0
                        if (enemyHealth.lifePoints <= 0)
                        {
                            Destroy(enemy.gameObject);
                        }
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Destroy the swatterPrefab (this instance) and increase quantity
            Destroy(gameObject);
            levelEditorManager.itemButtons[ID].quantity++;
            levelEditorManager.itemButtons[ID].quantityTxt.text = levelEditorManager.itemButtons[ID].quantity.ToString();
        }
    }
}
