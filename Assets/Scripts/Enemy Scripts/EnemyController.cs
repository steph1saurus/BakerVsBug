using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float initialSpeed;
    public float stoppingDistance = 0;
    private GameObject closestBakedGood;

    public bool touchingBakedGood = false;
    public bool touchingStickyTrap = false;

    private Coroutine damageCoroutine;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip eatingSound;

    private void Start()
    {
        initialSpeed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        closestBakedGood = FindClosestObject();
        if (closestBakedGood != null)
        {
            MoveTowardsClosestObject();
        }
    }

    public GameObject FindClosestObject()
    {
        GameObject[] gos;
        gos = GameObject.FindGameObjectsWithTag("BakedGood");
        GameObject closest = null;
        float distance = Mathf.Infinity;
        Vector3 position = transform.position;
        foreach (GameObject go in gos)
        {
            Vector3 diff = go.transform.position - position;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance)
            {
                closest = go;
                distance = curDistance;
            }
        }
        return closest;
    }

    private void MoveTowardsClosestObject()
    {
        if (closestBakedGood != null)
        {
            Vector3 direction = closestBakedGood.transform.position - transform.position;
            float distanceToTarget = direction.magnitude;

            Collider2D bakedGoodCollider = closestBakedGood.GetComponent<Collider2D>();
            if (bakedGoodCollider != null)
            {
                float colliderRadius = bakedGoodCollider.bounds.extents.magnitude;
                float totalStoppingDistance = colliderRadius + stoppingDistance;

                if (distanceToTarget > totalStoppingDistance)
                {
                    touchingBakedGood = false;
                    direction.Normalize();
                    transform.position += direction * speed * Time.deltaTime;

                    // Stop coroutine if the enemy moves away from the baked good
                    if (damageCoroutine != null)
                    {
                        StopCoroutine(damageCoroutine);
                        damageCoroutine = null;
                    }
                }
                else
                {
                    touchingBakedGood = true;

                    // Start the coroutine if touching the baked good
                    if (damageCoroutine == null)
                    {
                        damageCoroutine = StartCoroutine(ReduceLifeOverTime(closestBakedGood));
                    }
                }
            }
        }
    }

    // Coroutine to reduce life points over time (every 1 second)
    private IEnumerator ReduceLifeOverTime(GameObject bakedGood)
    {
        BakedGoodHealth bakedGoodHealth = bakedGood.GetComponent<BakedGoodHealth>();

        while (touchingBakedGood && bakedGoodHealth != null)
        {
            bakedGoodHealth.ReduceLife();
            // Play the eating sound as a one-shot
            if (audioSource != null && eatingSound != null)
            {
                audioSource.PlayOneShot(eatingSound);
            }

            yield return new WaitForSeconds(1f); // Wait for 1 second between life reduction
        }
    }
}
