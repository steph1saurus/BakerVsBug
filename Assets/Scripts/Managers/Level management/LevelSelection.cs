using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    private void Start()
    {
        // Assign the button onClick events to set the level number
        level1Button.onClick.AddListener(() => SetLevel(1));
        level2Button.onClick.AddListener(() => SetLevel(2));
        level3Button.onClick.AddListener(() => SetLevel(3));
    }

    private void SetLevel(int levelNum)
    {
        try
        {
            // Try to find the LevelManager in the scene
            LevelManager levelManager = FindObjectOfType<LevelManager>();

            if (levelManager != null)
            {
                levelManager.SetLevelNum(levelNum);
            }

            else
            {
                throw new System.Exception("LevelManager not found in current scene");
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError("LevelManager instance not found." + e.Message);
        }
    }
}

