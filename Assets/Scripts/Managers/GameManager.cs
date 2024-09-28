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

    [Header ("Variables")]
  


    [Header("Checks")]
    private bool levelCompleteBool = false;

    // Update is called once per frame
    void Update()
    {
        LevelComplete();

        GameObject[] bakedGoods = GameObject.FindGameObjectsWithTag("BakedGood");

        if (bakedGoods.Length ==0)
        {
            GameOver();
        }

    }


    private void LevelComplete()
    {
        if (progressBar.value ==1 && !levelCompleteBool)
        {
            levelCompleteBool = true;
            completionText.SetActive(true);
                
                Time.timeScale = 0;
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
    }


    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

}

