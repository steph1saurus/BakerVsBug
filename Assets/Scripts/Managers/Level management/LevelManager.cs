using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
   
    public int selectLevelNum;


    // Method to set the selected level number
    public void SetLevelNum(int levelNum)
    {
        selectLevelNum = levelNum;
        Debug.Log("Selected Level: " + selectLevelNum);
    }

}
