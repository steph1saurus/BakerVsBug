using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarItem : MonoBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DestroySugarTrap());
        
    }

    private IEnumerator DestroySugarTrap()
    {
        yield return new WaitForSeconds(5f);
        Destroy(gameObject);
    }


}
