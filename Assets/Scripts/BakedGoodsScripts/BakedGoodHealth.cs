
using UnityEngine;
using System.Collections;

public class BakedGoodHealth : MonoBehaviour
{
    [Header("Max health points")]
    public float lifePoints = 20f;

    [SerializeField] FloatingHealthBar healthBar;
 


    private bool isTakingDamage = false; // To track if the damage coroutine is running



    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();
      
    }

    private void Start()
    {
        healthBar.UpdateHealthBar(lifePoints);
        
    }

    private void Update()
    {
        CheckEnemiesTouching();
    }
    private void CheckEnemiesTouching()
    {
        // Find all objects with the "Enemy" tag in the scene
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        bool anyEnemyTouching = false;

        // Loop through each enemy to check if any has touchingBakedGood = true
        foreach (GameObject enemy in enemies)
        {
            AntController antController = enemy.GetComponent<AntController>();
            if (antController != null && antController.touchingBakedGood)
            {
                anyEnemyTouching = true;
                break; // Only need one touching enemy to start damage
            }
        }

        // Start or stop damage based on whether any enemy is touching
        if (anyEnemyTouching && !isTakingDamage)
        {
            isTakingDamage = true;
            StartCoroutine(DamageOverTime()); // Start taking damage if at least one enemy is touching
        }
        else if (!anyEnemyTouching && isTakingDamage)
        {
            isTakingDamage = false;
            StopCoroutine(DamageOverTime()); // Stop taking damage if no enemies are touching
        }

    }

    public void ReduceLife()
    {

        lifePoints -= 0.5f;
        healthBar.UpdateHealthBar(lifePoints);

        //destroy baked good when lifepoints = 0
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Baked good destroyed");
        }
    }

    
    // Coroutine to reduce life points every second
    private IEnumerator DamageOverTime()
    {
        while (isTakingDamage)
        {
            ReduceLife();
            yield return new WaitForSeconds(1f); // Deal damage every 1 second
        }
    }

    

}