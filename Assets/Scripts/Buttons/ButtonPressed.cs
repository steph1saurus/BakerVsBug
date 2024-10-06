
using UnityEngine;

public class ButtonPressed : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip clickSound;

    [SerializeField] SoundMixerManager soundMixerManager;
    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        clickSound = soundMixerManager.clickSound;
    }

    public void OnButtonPressed()
    {
        //clickSound = GameManager.GMinstance.clickSound;
        audioSource.PlayOneShot(clickSound);
    }
}
