
using UnityEngine;

public class MovePie : MonoBehaviour
{
    //Moving pie
    Vector3 offset;
    private bool moving = false;

    private void Update()
    {
        if (moving)
        {
            //move object, take into account original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }

    }


    private void OnMouseDown()
    {
        if (moving == false)
        {
            //take into account difference between object center and clicked point on camera plane
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;
        }
        else moving = false;
    }
    

}
