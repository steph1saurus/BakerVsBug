
using UnityEngine;
using UnityEngine.Audio;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public AudioSource clickSound;


    public void SetMainVolume(float level)
    {
        audioMixer.SetFloat("MasterVol", Mathf.Log10(level) * 20);
    }

    public void SetMusicVolume(float level)
    {
        audioMixer.SetFloat("Music", Mathf.Log10(level) * 20);

    }

    public void SetSFXVolume(float level)
    {
        audioMixer.SetFloat("SoundFX", Mathf.Log10(level) * 20);

    }
}
