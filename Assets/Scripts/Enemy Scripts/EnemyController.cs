using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public float initialSpeed;
    public float stoppingDistance = 0;
    private GameObject closestBakedGood;

    public bool touchingBakedGood = false;
    public bool touchingStickyTrap = false;


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
                }
                else
                {
                    touchingBakedGood = true;
                }
            }

        }
    }

 

}
