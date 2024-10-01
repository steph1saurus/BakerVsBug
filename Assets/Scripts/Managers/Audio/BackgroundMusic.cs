using UnityEngine;
using UnityEngine.SceneManagement;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic instance;

    private AudioSource audioSource;

    // Music clips for different scenes
    public AudioClip defaultMusic; // For title scene and other scenes
    public AudioClip sceneMusic;  // For main scene

    private string currentScene;

    void Awake()
    {
    
            // Get the AudioSource component and start the music
            audioSource = GetComponent<AudioSource>();

            // Play the default music initially
            PlayDefaultMusic();
    
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
