using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemController : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField] public int ID; // ID of the button

    [Header("Item quantity")]
    [SerializeField] public int quantity; // item quantity

    [Header("Quantity text")]
    [SerializeField] public TextMeshProUGUI quantityTxt; // quantity text

    [Header("Button clicked")]
    [SerializeField] public bool clicked = false;
    public Button button; // reference the item button in editor

    [Header("Level Editor Manager")]
    [SerializeField] LevelEditorManager levelEditorManager;

    void Start()
    {
        // Retrieve the quantity from PlayerPrefs and set it
        quantity = PlayerPrefs.GetInt($"Inventory_{ID}");
        quantityTxt.text = quantity.ToString();

        levelEditorManager = GameObject.FindGameObjectWithTag("LevelEditorManager").GetComponent<LevelEditorManager>();
    }

    public void ButtonClicked()
    {
        if (quantity > 0)
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            Instantiate(levelEditorManager.itemImage[ID], new Vector3(worldPosition.x, worldPosition.y, 0), Quaternion.identity);

            clicked = true;

            quantity--;
            quantityTxt.text = quantity.ToString();

            // Update PlayerPrefs to reflect the new quantity
            PlayerPrefs.SetInt($"Inventory_{ID}", quantity);
            PlayerPrefs.Save();

            levelEditorManager.currentButtonPressed = ID;
            Debug.Log("ButtonClicked" + ID);
        }
    }
}
