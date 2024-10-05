using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class GameManager: MonoBehaviour
{
    public static GameManager GMinstance;
    [Header("Checks")]
    public bool levelCompleteBool = false;
    public bool isPaused = false;

    [Header("AudioClips")]
    public AudioClip clickSound;
    public AudioClip pauseSound;
    public AudioClip gameOverSound;
    public AudioClip levelCompleteSound;


    private void Awake()
    {
        if (GMinstance == null)
        {
            GMinstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }


    public void LoadScene(string scene)
    {
        WaitToLoadScene();
        SceneManager.LoadScene(scene);
    }


    public void Restart()
    {
        WaitToLoadScene();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        LoadScene("TitleScene");
    }

    public void OnApplicationQuit()
    {
        
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    public void TimePaused()
    {
        if (!isPaused)
        {
            isPaused = true;
            Time.timeScale = 0f;
            Debug.Log(Time.timeScale);
        }
        else
        {
            isPaused = false;
            Time.timeScale = 1f;
        }    
       
    }

    public IEnumerator WaitToLoadScene()
    {
        yield return new WaitForSecondsRealtime(1f);
    }

}

