using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SoundMixerManager : MonoBehaviour
{
    [SerializeField] public static SoundMixerManager SMMinstance;

    [Header("Audio Source")]
    [SerializeField] AudioMixer audioMixer;

    [SerializeField] AudioSource audioSource;

    [SerializeField] public AudioMixerGroup musicMixerGroup;
    [SerializeField] public string musicVolumeParameter = "Music";

    [Header("Music")]
    // Music clips for different scenes
    [SerializeField] public AudioClip defaultMusic; // For title scene and other scenes
    [SerializeField] public AudioClip sceneMusic;  // For main scene


    //Game Manager items
    [Header("Level complete or paused")]
    [SerializeField] public bool levelCompleteBool = false;
    [SerializeField] public bool isPaused = false;

    [Header("General Game Sounds")]
    [SerializeField] public AudioClip clickSound;
    [SerializeField] public AudioClip pauseSound;
    [SerializeField] public AudioClip gameOverSound;
    [SerializeField] public AudioClip levelCompleteSound;

    [Header("Action sounds")]
    [SerializeField] public AudioClip eatSound;
    [SerializeField] public AudioClip swatterSound;
    [SerializeField] public AudioClip explosionSound;
    [SerializeField] public AudioClip stickySound;
    [SerializeField] public AudioClip spraySound;
    [SerializeField] public AudioClip coinSound;


    [Header("CurrentScene")]
    private string currentScene;

    private void Awake()
    {
        if (SMMinstance == null)
        {
            SMMinstance = this;
            DontDestroyOnLoad(gameObject);
            // Get the AudioSource component and start the music
            audioSource = gameObject.GetComponent<AudioSource>();

            // Assign the output to the Music group in the AudioMixer
            audioSource.outputAudioMixerGroup = musicMixerGroup;

            // Play the default music initially
            PlayDefaultMusic();
        }
        else Destroy(gameObject);

    }

    
    public void OnEnable()
    {
        // Subscribe to scene load event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
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

    public void LoadScene(string scene)
    {
        StartCoroutine(WaitToLoadScene());
        SceneManager.LoadScene(scene);
    }


    public void Restart()
    {
        StartCoroutine(WaitToLoadScene());
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        LoadScene("TitleScene");
    }


    public void QuitApplication()
    {
        Application.Quit();
    }

    public void TimePaused()
    {
        if (!isPaused)
        {
            audioSource.Pause();
            isPaused = true;
            Time.timeScale = 0f;
            Debug.Log(Time.timeScale);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
            audioSource.UnPause();
        }

    }

    public IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSeconds(1f);
    }

}