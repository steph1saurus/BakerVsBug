using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SelectDifficultyScript : MonoBehaviour
{
    [SerializeField] Button button;
    [SerializeField] public int difficulty; //easy = 1, normal = 2, hard = 3


    [Header("PlayerPref setting")]
    [SerializeField] private const string difficultyKey = "difficultyKey";

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Saved difficulty" + difficultyKey);
        int savedDifficulty = PlayerPrefs.GetInt(difficultyKey, 1);
        SetDifficulty(savedDifficulty);

    }

    public void SetDifficulty(int choice)
    {
        difficulty = choice;
        PlayerPrefs.SetInt("difficultyKey", choice);
        Debug.Log("Difficulty Selected" + choice);
    }
 }
