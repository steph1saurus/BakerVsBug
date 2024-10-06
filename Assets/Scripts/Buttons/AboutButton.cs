using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AboutButton : MonoBehaviour
{
    [SerializeField] SoundMixerManager soundMixerManager;


    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();

    }

    public void AboutButtonPressed()
    {
        soundMixerManager.LoadScene("CreditsScene");
    }
}
