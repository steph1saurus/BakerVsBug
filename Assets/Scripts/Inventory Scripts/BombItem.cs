using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombItem : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {

        StartCoroutine(CountDownToExplode());
    }

    private IEnumerator CountDownToExplode()
    {
        yield return new WaitForSeconds(5f);

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject enemy in enemies)
        {
            Destroy(enemy);
        }
        Destroy(gameObject);
        
    }
}
