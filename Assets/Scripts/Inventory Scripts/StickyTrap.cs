using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StickyTrap : MonoBehaviour
{
    [Header("Variables")]
    public float speedReductionFactor = 0.5f;
    public float effectDuration = 5f;

    [Header("Game Objects")]
    public GameObject stickyTrapText;
    

    public void EnableStickyTrap()
    {
        stickyTrapText.SetActive(true);
        FindObjectsOfType<AntController>();

        AntController enemyController = GetComponent<AntController>();
        if (enemyController != null)
        {
            StartCoroutine(ApplySpeedReduction(enemyController));
        }
    }


    // Coroutine to apply speed reduction and then restore the original speed after the effect duration
    private IEnumerator ApplySpeedReduction(AntController enemy)
    {
        
        float originalSpeed = enemy.speed; // Store the original speed
        enemy.speed *= speedReductionFactor; // Reduce speed by half

        yield return new WaitForSeconds(effectDuration); // Wait for the effect duration

        enemy.speed = originalSpeed; // Restore the original speed
        stickyTrapText.SetActive(false);
    }
}
