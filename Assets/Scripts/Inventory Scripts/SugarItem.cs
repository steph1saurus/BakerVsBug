using System.Collections;
using UnityEngine;

public class SugarItem : MonoBehaviour
{
    [Header("Item ID")]
    public int ID;

    [Header("time to wait")]
    [SerializeField] float timeToWait = 5f;

    void Start()
    {
        StartCoroutine(DestroySugarTrap());
        
    }
    //Destroy Sugar trap after set amount of time
    private IEnumerator DestroySugarTrap()
    {
        yield return new WaitForSeconds(timeToWait);
        Destroy(gameObject);
    }


}
