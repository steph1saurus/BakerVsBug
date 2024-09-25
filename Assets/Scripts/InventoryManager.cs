
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject swatterPrefab;


    [Header("Variables")]
    public bool swatterSelected = false;


    public void OnMouseDown()
    {
        if (!swatterSelected)
        {
            ActivateSwatter();
        }
        else if (swatterSelected)
        {
            DeactivatSwatter();
        }
    }


    void ActivateSwatter()
    {
        swatterSelected = true;
        GameObject.Instantiate(swatterPrefab);
    }

    void DeactivatSwatter()
    {
        swatterSelected = false;
        //destroy clone of swatterPrefab
    }


}
