using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] SoundMixerManager soundMixerManager;


    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();

    }

    public void StartButtonPressed()
    {
        soundMixerManager.LoadScene("MainScene");
    }
}
