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

    [Header ("Variables")]
  


    [Header("Checks")]
    private bool levelCompleteBool = false;

    // Update is called once per frame
    void Update()
    {
        LevelComplete();
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

        // Load the reward scene (with build index 1)
        SceneManager.LoadScene(1);  // Build index 1 is the Reward scene
    }

}

