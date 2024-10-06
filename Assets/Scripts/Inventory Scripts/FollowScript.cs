using UnityEngine;


//----Used for moving prefabImages with the mouse----//
public class FollowScript : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 worlPosition = Camera.main.ScreenToWorldPoint(screenPosition);

        transform.position = worlPosition;
    }
}
