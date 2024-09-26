using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBakedGoods : MonoBehaviour
{
    public GameObject[] bakedGoods;
    private Vector3 spawnPosition;



    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = new Vector3(0, 0, 0);
        Instantiate(bakedGoods[0], spawnPosition, Quaternion.identity);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
