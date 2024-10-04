
using UnityEngine;


public class QuitLevel : MonoBehaviour
{

    [SerializeField] GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
  
    }

   public void PressQuitLevelButton()
   {
        
        gameManager.LoadScene("TitleScene");
   }

    
}
