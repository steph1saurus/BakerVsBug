using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameManager : MonoBehaviour
{
    [Header("Game objects")]
    public Slider progressBar;
    public GameObject completionText;
    public GameObject gameOverScreen;

    [Header("Checks")]
    public bool levelCompleteBool = false;
    private bool isPaused = false;

    public static GameManager instance;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (!isPaused)
        {
            Time.timeScale = 1f;

            LevelComplete();

            GameObject[] bakedGoods = GameObject.FindGameObjectsWithTag("BakedGood");

            if (bakedGoods.Length == 0)
            {
                GameOver();
            }
        }
        else if (isPaused)
        {
            TimePaused();
        }

    }


    public void LevelComplete()
    {
        if (progressBar.value == 1 && !levelCompleteBool)
        {
            levelCompleteBool = true;
            completionText.SetActive(true);

            TimePaused();
            StartCoroutine(HandleLevelCompletion());
        }
    }

    private IEnumerator HandleLevelCompletion()
    {


        // Wait for 3 seconds
        yield return new WaitForSecondsRealtime(3f);  // Realtime because Time.timeScale is set to 0

        LoadScene("RewardScene");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        levelCompleteBool = true;
        TimePaused();
        StartCoroutine(HandleLevelCompletion());
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        LoadScene("TitleScene");
    }

    private void TimePaused()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;
    }

    

}

