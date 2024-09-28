using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    public void StartButtonSelected()
    {
        LoadScene("MainScene");
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }
}
