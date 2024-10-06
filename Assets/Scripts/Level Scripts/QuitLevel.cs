
using UnityEngine;


public class QuitLevel : MonoBehaviour
{

    [SerializeField] SoundMixerManager soundMixerManager;
    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();

    }

    public void PressQuitLevelButton()
   {
        
        soundMixerManager.LoadScene("TitleScene");
   }

    
}
