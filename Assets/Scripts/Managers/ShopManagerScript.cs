using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[6,6];
    public int coins;//link to currency system?

    public TextMeshProUGUI coinsTxt;
  

    // Start is called before the first frame update
    void Start()
    {
        coinsTxt.text = "Wallet: " + coins.ToString();

        //shop item array
        shopItems[1, 1] = 1;
        shopItems[1, 2] = 2;
        shopItems[1, 3] = 3;
        shopItems[1, 4] = 4;
        shopItems[1, 5] = 5;

        //price
        shopItems[2, 1] = 20;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 100;
        shopItems[2, 5] = 500;

        //quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 0;
        shopItems[3, 3] = 0;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 0;
    }
    
    // Update is called once per frame
    void Update()
    {


    }

    public void Buy()
    {
        GameObject buttonRef = GameObject.FindGameObjectWithTag("Event").GetComponent<EventSystem>().currentSelectedGameObject;

        //check if have enough coins
        if(coins >=shopItems[2,buttonRef.GetComponent<ButtonInfo>().itemID])
        {
            coins -= shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID]; //subtract shopItem price from coins

            shopItems[3, buttonRef.GetComponent<ButtonInfo>().itemID] ++; //update quantity

            coinsTxt.text = "Wallet: " + coins.ToString();
            buttonRef.GetComponent<ButtonInfo>().quantityText.text = shopItems[2, buttonRef.GetComponent<ButtonInfo>().itemID].ToString();

        }

    }
}
