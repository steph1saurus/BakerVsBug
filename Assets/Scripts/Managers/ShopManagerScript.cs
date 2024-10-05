using UnityEngine.EventSystems;
using UnityEngine;
using TMPro;

public class ShopManagerScript : MonoBehaviour
{
    public int[,] shopItems = new int[5,5];
    public int coins;//link to currency system?

    public TextMeshProUGUI coinsTxt;
  

    // Start is called before the first frame update
    void Start()
    {
        //coins = PlayerPrefs.GetInt("CurrencyBalance", 0);

        coinsTxt.text = "Wallet: " + coins.ToString();

        //shop item array
        shopItems[1, 1] = 1;//swatter
        shopItems[1, 2] = 2;//sticky
        shopItems[1, 3] = 3;//sugar
        shopItems[1, 4] = 4;//big swatter
        shopItems[1, 5] = 5;//bomb
        shopItems[1, 6] = 6;//spray


        //price
        shopItems[2, 1] = 0;
        shopItems[2, 2] = 50;
        shopItems[2, 3] = 30;
        shopItems[2, 4] = 0;
        shopItems[2, 5] = 50;
        shopItems[2, 6] = 100;


        //quantity
        shopItems[3, 1] = 0;
        shopItems[3, 2] = 2;
        shopItems[3, 3] = 2;
        shopItems[3, 4] = 0;
        shopItems[3, 5] = 1;
        shopItems[3, 6] = 1;
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


            // Save the updated coins value back to PlayerPrefs
            PlayerPrefs.SetInt("CurrencyBalance", coins);
            PlayerPrefs.Save(); // Make sure to save the changes
        }

    }
}
