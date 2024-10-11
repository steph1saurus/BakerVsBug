
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class MusicVolControlScript : MonoBehaviour
{
    [Header("Audio source")]
    [SerializeField] AudioSource audioSource;

    [Header("AudioMixer")]
    [SerializeField] AudioMixer audioMixer;

    [Header("Slider")]
    [SerializeField] Slider musicSlider;

    [Header("Player pref settings")]
    [SerializeField] private const string MusicVolumeKey = "MusicVolKey";

    // Start is called before the first frame update
    void Start()
    {
        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        musicSlider.value = savedMusicVolume;
        SetMusicVolume(savedMusicVolume);
    }

 

    public void SetMusicVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("Music", volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, level);
        PlayerPrefs.Save();
    }
}

