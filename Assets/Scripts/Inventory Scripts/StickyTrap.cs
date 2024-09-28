using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyTrap : MonoBehaviour
{
    public float speedReductionFactor = 0.5f;
    public float effectDuration = 5f;


    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Enemy"))
        {
            AntController enemyController = other.GetComponent<AntController>();
            if (enemyController != null)
            {
                StartCoroutine(ApplySpeedReduction(enemyController));
            }

        }
    }


    // Coroutine to apply speed reduction and then restore the original speed after the effect duration
    private IEnumerator ApplySpeedReduction(AntController enemy)
    {
        float originalSpeed = enemy.speed; // Store the original speed
        enemy.speed *= speedReductionFactor; // Reduce speed by half

        yield return new WaitForSeconds(effectDuration); // Wait for the effect duration

        enemy.speed = originalSpeed; // Restore the original speed
    }
}
