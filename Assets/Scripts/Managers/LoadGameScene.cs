
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadGameScene : MonoBehaviour
{
   
    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

 
}
