using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public ItemController[] itemButtons; // Array of item buttons
    public GameObject[] itemPrefabs; // Array of item prefabs
    public GameObject[] itemImage;


    public int currentButtonPressed; // Reference buttonID

  
        private void Update()
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);

            if (Input.GetMouseButtonDown(0) && itemButtons[currentButtonPressed].clicked)
            {
                itemButtons[currentButtonPressed].clicked = false;
                Instantiate(itemPrefabs[currentButtonPressed], new Vector3(worlPosition.x, worlPosition.y, 0), Quaternion.identity);
                Destroy(GameObject.FindGameObjectWithTag("ItemImage"));
            }

        }
    
    }


