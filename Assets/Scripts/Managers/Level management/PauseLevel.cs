
using UnityEngine;

public class PauseLevel : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pauseSound;

    [SerializeField] GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        
    }

   public void PressPauseButton()

    { 
        gameManager.TimePaused();
    }

    public void BackToLevelButton()
    {
        gameManager.TimePaused();
    }

}
