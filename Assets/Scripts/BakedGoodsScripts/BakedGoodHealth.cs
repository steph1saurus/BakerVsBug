using UnityEngine;


public class BakedGoodHealth : MonoBehaviour
{
    [Header("Max health points")]
    [SerializeField] public float initialLifePoints = 20f;
    [SerializeField] public float currentLifePoints;

    [Header("Payout amount")]
    [SerializeField] public int levelPayout;

    [Header("Health bar")]
    [SerializeField] FloatingHealthBar healthBar;

    [SerializeField] public bool isTakingDamage = false; // To track if the damage coroutine is running


    private void Awake()
    {
        healthBar = GetComponentInChildren<FloatingHealthBar>();

    }

    private void Start()
    {
        currentLifePoints = initialLifePoints;
        healthBar.UpdateHealthBar(currentLifePoints);

    }


    public void ReduceLife()
    {
        isTakingDamage = true;
        currentLifePoints -= 0.5f;
        healthBar.UpdateHealthBar(currentLifePoints);

        //destroy baked good when lifepoints = 0
        if (currentLifePoints <= 0)
        {
            Destroy(gameObject);
            Debug.Log("Baked good destroyed");
        }
    }

}