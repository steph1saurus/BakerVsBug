
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemController : MonoBehaviour
{
    public int ID; //ID of the button
    public int quantity;
    public TextMeshProUGUI quantityTxt;
    public bool clicked = false;

    public Button button;

    [Header("Level Editor Manager")]
    private LevelEditorManager levelEditorManager;

    // Start is called before the first frame update
    void Start()
    {
        quantityTxt.text = quantity.ToString();
        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();

    }

    public void ButtonClicked()
    {
        if (quantity >0)
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
