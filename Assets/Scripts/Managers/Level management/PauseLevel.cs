
using UnityEngine;

public class PauseLevel : MonoBehaviour
{

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pauseSound;

    [SerializeField] SoundMixerManager soundMixerManager;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();

    }

    public void PressPauseButton()

    { 
        soundMixerManager.TimePaused();
    }

    public void BackToLevelButton()
    {
        soundMixerManager.TimePaused();
    }

}
