
using UnityEngine;

public class BakedGoodHealth : MonoBehaviour
{
    public int lifePoints = 20;

    public void ReduceLife()
    {
        lifePoints -= 1;

        //destroy enemy when lifepoints = 0
        if (lifePoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Baked good destroyed");
        }
    }
}