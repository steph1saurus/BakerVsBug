
using UnityEngine;

public class BackToTitle : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

   public void BackButtonPressed()
    {
        gameManager.LoadScene("TitleScene");
    }
}
