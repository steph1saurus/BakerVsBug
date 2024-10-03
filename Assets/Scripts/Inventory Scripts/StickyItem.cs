using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickyItem : MonoBehaviour
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
            Destroy(gameObject);
            levelEditorManager.itemButtons[ID].quantity++;
            levelEditorManager.itemButtons[ID].quantityTxt.text = levelEditorManager.itemButtons[ID].quantity.ToString();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<EnemyController>().speed = 0;
        }
    }

  


}
