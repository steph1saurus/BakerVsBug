
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SoundMixerManager : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] public AudioSource clickSound;
    [SerializeField] private AudioSource audioSource;

    [SerializeField] public AudioMixerGroup musicMixerGroup;
    [SerializeField] public string musicVolumeParameter = "Music";

    [Header("Sliders")]
    [SerializeField] Slider mainSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider SFXSlider;

    [Header("PlayerPref settings")]
    private const string MainVolumeKey = "MainVolKey";
    private const string MusicVolumeKey = "MusicVolKey";
    private const string SFXVolumeKey = "SFXVolKey";

    [Header ("AudioClips")]
    // Music clips for different scenes
    public AudioClip defaultMusic; // For title scene and other scenes
    public AudioClip sceneMusic;  // For main scene

    [Header("CurrentScene")]
    private string currentScene;

    private void Awake()
    {
        // Get the AudioSource component and start the music
        audioSource = gameObject.GetComponent<AudioSource>();

        // Assign the output to the Music group in the AudioMixer
        audioSource.outputAudioMixerGroup = musicMixerGroup;

        // Play the default music initially
        PlayDefaultMusic();

    }

    private void Start()
    {
        float savedMainVolume = PlayerPrefs.GetFloat(MainVolumeKey, 1.0f);
        mainSlider.value = savedMainVolume;
        SetMainVolume(savedMainVolume);

        float savedMusicVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 1.0f);
        musicSlider.value = savedMusicVolume;
        SetMusicVolume(savedMusicVolume);

        float savedSFXVolume = PlayerPrefs.GetFloat(SFXVolumeKey, 1.0f);
        SFXSlider.value = savedSFXVolume;
        SetSFXVolume(savedSFXVolume);
    }

    public void SetMainVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("MasterVol", volume);
        PlayerPrefs.SetFloat(MainVolumeKey, level);
        PlayerPrefs.Save();
    }

    public void SetMusicVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("Music",volume);
        PlayerPrefs.SetFloat(MusicVolumeKey, level);
        PlayerPrefs.Save();

    }

    public void SetSFXVolume(float level)
    {
        float volume = Mathf.Log10(level) * 20;
        audioMixer.SetFloat("SoundFX", volume);
        PlayerPrefs.SetFloat(SFXVolumeKey, level);
        PlayerPrefs.Save();

    }

    void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe to prevent memory leaks
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // Called when a new scene is loaded
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        currentScene = scene.name;

        // Check if we're in Scene 4 and need to switch music
        if (currentScene == "MainScene")
        {
            PlaySceneMusic();
        }
        else
        {
            PlayDefaultMusic();
        }
    }

    // Plays the default music for Scenes 1, 2, 3
    private void PlayDefaultMusic()
    {
        if (audioSource.clip != defaultMusic)
        {
            audioSource.clip = defaultMusic;
            audioSource.Play();
        }
    }

    // Plays a different music for Scene 4
    private void PlaySceneMusic()
    {
        if (audioSource.clip != sceneMusic)
        {
            audioSource.clip = sceneMusic;
            audioSource.Play();
        }
    }

}
