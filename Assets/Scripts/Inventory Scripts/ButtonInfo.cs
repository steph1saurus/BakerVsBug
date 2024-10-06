using UnityEngine;
using TMPro;

public class ButtonInfo : MonoBehaviour
{
    [Header("Item ID")]
    [SerializeField] public int itemID;

    [Header("Price")]
    [SerializeField] public TextMeshProUGUI priceText;
    [Header("Quantity")]
    [SerializeField] public TextMeshProUGUI quantityText;

    [SerializeField] GameObject shopManager;


    private void Update()
    {
        priceText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
        quantityText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[3, itemID].ToString();
    }
}
