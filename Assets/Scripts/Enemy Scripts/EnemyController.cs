using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Move and check for baked goods")]
    [SerializeField] public float speed;
    [SerializeField] public float initialSpeed;
    [SerializeField] public float stoppingDistance = 0;
    [SerializeField] GameObject closestBakedGood;

    [Header("Check for touching items")]
    [SerializeField] public bool touchingBakedGood = false;
    [SerializeField] public bool touchingStickyTrap = false;


    [Header("Audio")]
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip eatingSound;

    [SerializeField] SoundMixerManager soundMixerManager;
    [SerializeField] Coroutine damageCoroutine;

    private void Start()
    {
        initialSpeed = speed;
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>(); //reference SoundMixerManager
        eatingSound = soundMixerManager.eatSound;
    }

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
