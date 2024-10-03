using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwatterItem : MonoBehaviour
{
    public int ID;
    private LevelEditorManager levelEditorManager;

    // Start is called before the first frame update
    void Start()
    {
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Destroy(this.gameObject);
            levelEditorManager.itemButtons[ID].quantity++;
            levelEditorManager.itemButtons[ID].quantityTxt.text = levelEditorManager.itemButtons[ID].quantity.ToString();
        }
    }
}
