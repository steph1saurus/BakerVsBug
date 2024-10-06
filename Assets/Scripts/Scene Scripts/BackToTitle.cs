
using UnityEngine;

public class BackToTitle : MonoBehaviour
{
    [SerializeField] SoundMixerManager soundMixerManager;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
    }

    public void BackButtonPressed()
    {
        soundMixerManager.LoadScene("TitleScene");
    }
}
