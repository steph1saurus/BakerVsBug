
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    //Moving pie
    public GameObject selectedObject;
    Vector3 offset;


    private void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.WorldToScreenPoint(selectedObject.transform.position).z));


        if (Input.GetMouseButtonDown(0))
            {
                Collider2D targetObject = Physics2D.OverlapPoint(mousePosition);

                if (targetObject)
                {
                    selectedObject = targetObject.transform.gameObject;
                    offset = selectedObject.transform.position - mousePosition;
                }
            }

            if (selectedObject)
            {
            selectedObject.transform.position = new Vector3(mousePosition.x, mousePosition.y, selectedObject.transform.position.z) + offset;
        }

        if (Input.GetMouseButtonUp(0) && selectedObject)
            {
                selectedObject = null;
            }
        


    }


}
