using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveItem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseDown()
    {


        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);
    }

    void OnMouseDrag()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);
      
    }
}
