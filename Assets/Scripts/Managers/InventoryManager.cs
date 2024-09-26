
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public GameObject swatterPrefab; //reference to the prefab asset
    private GameObject instantiatedSwatter; //reference to the instantiated swatter


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
        instantiatedSwatter = GameObject.Instantiate(swatterPrefab);
    }

    void DeactivatSwatter()
    {
        swatterSelected = false;

        if (instantiatedSwatter != null)
        {
            Destroy(instantiatedSwatter);
        }
    }


}
