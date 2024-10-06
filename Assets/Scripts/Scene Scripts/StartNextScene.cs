
using UnityEngine;

public class StartNextScene : MonoBehaviour
{
    [SerializeField] SoundMixerManager soundMixerManager;

    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
    }

    public void StartNextSceneButtonPressed()
    {
        soundMixerManager.LoadScene("MainScene");

    }
}
