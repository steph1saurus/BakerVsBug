
using UnityEngine;

public class StartNextScene : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    public void StartNextSceneButtonPressed()
    {
        gameManager.LoadScene("MainScene");

    }
}
