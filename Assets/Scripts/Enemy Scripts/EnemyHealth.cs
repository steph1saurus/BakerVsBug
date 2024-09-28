
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int lifePoints;

    public void ReduceLife (int amount)
    {
        lifePoints -= amount;

        //destroy enemy when lifepoints = 0
        if (lifePoints <=0)
        {
            Destroy(gameObject);
            Debug.Log("enemy destroyed");
        }
    }


}
