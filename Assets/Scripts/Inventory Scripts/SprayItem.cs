using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SprayItem : MonoBehaviour
{
    public int ID;
    public GameObject sprayEffectPrefab; // Assign your particle effect prefab in the Inspector
    private LevelEditorManager levelEditorManager;
    [SerializeField] SoundMixerManager soundMixerManager;

    [Header("Spray effect")]
    private float holdTime = 0f; // Time the button is held down
    private bool isHolding = false; // To track if the button is being held
    private GameObject activeSprayEffect; // To keep track of the instantiated particle effect
    public Vector3 sprayEffectOffset = new Vector3(-2f, 0f, 0f); // Offset for the particle effect

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip spraySound;

    // Start is called before the first frame update
    void Start()
    {
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        spraySound = soundMixerManager.spraySound;
    }

    // Update is called once per frame
    void Update()
    {
        // Update spray position to follow the mouse
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we are in 2D
        transform.position = mousePosition;

        // Check if the left mouse button is held down
        if (Input.GetMouseButton(0))
        {
            holdTime += Time.deltaTime; // Increment hold time
            isHolding = true;

            // Play the spray sound on loop
            if (audioSource != null && spraySound != null && !audioSource.isPlaying)
            {
                audioSource.loop = true; // Enable looping
                audioSource.clip = spraySound;
                audioSource.Play(); // Start playing sound
            }

            // Check for enemies within the trigger area
            Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(transform.position, 1f); // Adjust radius as needed

            foreach (Collider2D enemy in hitEnemies)
            {
                if (enemy.CompareTag("Enemy"))
                {
                    // Reduce enemy health by 3
                    EnemyHealth enemyHealth = enemy.GetComponent<EnemyHealth>();
                    if (enemyHealth != null)
                    {
                        enemyHealth.lifePoints -= 3;

                        // Optionally destroy the enemy if health reaches 0
                        if (enemyHealth.lifePoints <= 0)
                        {
                            Destroy(enemy.gameObject);
                        }
                    }
                }
            }

            // Instantiate the particle effect if hold time is less than 5 seconds
            if (holdTime < 5f && activeSprayEffect == null)
            {
               
                activeSprayEffect = Instantiate(sprayEffectPrefab, transform.position + sprayEffectOffset, Quaternion.identity);
            }

            // Update the position of the particle effect to follow the mouse with an offset
            if (activeSprayEffect != null)
            {
                activeSprayEffect.transform.position = transform.position + sprayEffectOffset;
            }

            // Destroy the spray item if held for more than 5 seconds
            if (holdTime >= 10f)
            {
                DestroySprayItem();
            }
        }

        // Reset hold time when mouse button is released
        if (Input.GetMouseButtonUp(0))
        {
            if (isHolding)
            {
                isHolding = false; // Reset holding state
                StartCoroutine(ResetHoldTimeAfterDelay(3f)); // Start coroutine to reset hold time

                // Stop the spray sound
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    audioSource.loop = false; // Disable looping
                }

                // Stop the spray effect if it's active
                if (activeSprayEffect != null)
                {
                    Destroy(activeSprayEffect); // Destroy the particle effect
                    activeSprayEffect = null; // Reset the reference
                }
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            // Destroy the spray item and increase quantity
            Destroy(activeSprayEffect);
            // Destroy the spray item
            Destroy(gameObject);

            // Increase quantity in the LevelEditorManager
            levelEditorManager.itemButtons[ID].quantity++;
            levelEditorManager.itemButtons[ID].quantityTxt.text = levelEditorManager.itemButtons[ID].quantity.ToString();
        }
    }

    private void DestroySprayItem()
    {
        Destroy(activeSprayEffect);
        // Destroy the spray item
        Destroy(gameObject);

    }

    private IEnumerator ResetHoldTimeAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); // Wait for 3 seconds
        holdTime = 0f; // Reset the hold time
    }


}
