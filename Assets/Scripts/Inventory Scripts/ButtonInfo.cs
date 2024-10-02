using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ButtonInfo : MonoBehaviour
{

    public int itemID;
    public TextMeshProUGUI priceText;
    public TextMeshProUGUI quantityText;
    public GameObject shopManager;


    //private void Start()
    //{
    //    priceText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
    //    quantityText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[3, itemID].ToString();
    //}

    private void Update()
    {
        priceText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[2, itemID].ToString();
        quantityText.text = shopManager.GetComponent<ShopManagerScript>().shopItems[3, itemID].ToString();
    }
}
