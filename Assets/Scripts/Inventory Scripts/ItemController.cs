
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemController : MonoBehaviour
{
    public int ID; //ID of the button
    public int quantity; //item quantity
    public TextMeshProUGUI quantityTxt; //quantity text
    public bool clicked = false;

    public Button button; //reference the item button in editor

    [Header("Level Editor Manager")]
    private LevelEditorManager levelEditorManager;


    void Start()
    {
        quantityTxt.text = quantity.ToString();
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();

    }

    public void ButtonClicked()
    {
            if (quantity > 0)
            {
                Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);

                Instantiate(levelEditorManager.itemImage[ID], new Vector3(worlPosition.x, worlPosition.y, 0), Quaternion.identity);

                clicked = true;
                
                quantity--;
                quantityTxt.text = quantity.ToString();

                levelEditorManager.currentButtonPressed = ID;
                Debug.Log("ButtonClicked" + ID);
            }
        
    }


}
