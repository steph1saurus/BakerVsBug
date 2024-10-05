
using UnityEngine;

public class PauseLevel : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] SoundMixerManager soundMixerManager;

    [Header("Pause panel")]
    [SerializeField] GameObject pausePanel;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        pauseSound = soundMixerManager.pauseSound;
    }

    public void PressPauseButton()

    {
        pausePanel.SetActive(true);
        audioSource.PlayOneShot(pauseSound);
        soundMixerManager.TimePaused();
    }

    public void BackToLevelButton()
    {
        pausePanel.SetActive(false);
        soundMixerManager.TimePaused();
    }

}
