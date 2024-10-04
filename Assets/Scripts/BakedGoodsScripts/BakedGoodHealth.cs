using UnityEngine;
using System.Collections;

public class BakedGoodHealth : MonoBehaviour
{
    [Header("Max health points")]
    public float initialLifePoints = 20f;
    public float currentLifepoints;

    [Header("Payout amount")]
    public int levelPayout;

    [SerializeField] FloatingHealthBar healthBar;

    public bool isTakingDamage = false; // To track if the damage coroutine is running


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();

    }

    private void Start()
    {
        currentLifepoints = initialLifePoints;
        healthBar.UpdateHealthBar(currentLifepoints);

    }


    public void ReduceLife()
    {
        isTakingDamage = true;
        currentLifepoints -= 0.5f;
        healthBar.UpdateHealthBar(currentLifepoints);

        //destroy baked good when lifepoints = 0
        if (currentLifepoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Baked good destroyed");
        }
    }

}