using UnityEngine;

public class QuitGame : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] SoundMixerManager soundMixerManager;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>(); 
    }

    public void QuitGameButtonPressed()
    {
        soundMixerManager.QuitApplication();
    }
}
