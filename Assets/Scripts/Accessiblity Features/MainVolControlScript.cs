
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MainVolControlScript : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] AudioSource audioSource;

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Slider")]
    [SerializeField] Slider mainSlider;

    [Header("Player pref settings")]
    [SerializeField] private const string MainVolumeKey = "MainVolKey";

   
    void Start()
    {
        float savedMainVolume = PlayerPrefs.GetFloat(MainVolumeKey, 1.0f);
        mainSlider.value = savedMainVolume;
        SetMainVolume(savedMainVolume);

    }

    public void SetMainVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat(MainVolumeKey, level);
        PlayerPrefs.Save();
    }
}
