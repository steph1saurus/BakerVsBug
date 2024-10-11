
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class SoundFxControlScript : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] AudioSource audioSource;

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Slider")]
    [SerializeField] Slider SFXSlider;

    [Header("Player pref settings")]
    [SerializeField] private const string SFXVolumeKey = "SFXVolKey";

    // Start is called before the first frame update
    void Start()
    {
        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);
        SFXSlider.value = savedSFXVolume;
        SetSFXVolume(savedSFXVolume);
    }



    public void SetSFXVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("SoundFX", volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, level);
        PlayerPrefs.Save();
    }
}

