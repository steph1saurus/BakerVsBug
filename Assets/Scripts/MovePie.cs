
using UnityEngine;

public class MovePie : MonoBehaviour
{
    public bool moving = false;
    private Vector3 offset;

    

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            //move the pie, taking into account the original offset
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnMouseDown()
    {
        if (moving == false)
        {
            //record the difference between the centers of the pie and the clicked point on the camera plane
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            moving = true;

        }


        else moving = false;
    }
}
