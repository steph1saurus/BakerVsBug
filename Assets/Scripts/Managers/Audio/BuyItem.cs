using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyItem : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioClip coinSound;
    [SerializeField] SoundMixerManager soundMixerManager;
    // Start is called before the first frame update
    void Start()
    {
        soundMixerManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<SoundMixerManager>();
        coinSound = soundMixerManager.coinSound;
    }

    public void ItemClicked()
    {
        audioSource.PlayOneShot(coinSound);
    }
}
