
using UnityEngine;

public class RotateImageLeft : MonoBehaviour
{
    [SerializeField] public float rotationAngle = 15f; // Maximum angle of rotation
    [SerializeField] public float rotationSpeed = 2f;  // Speed of the rotation

    [SerializeField] float currentRotation = 0f;
    [SerializeField] bool rotatingLeft = true;

    [Header("Initial Rotation")]
    [SerializeField] float initialYRotation;
    [SerializeField] float initialZRotation;

    private void Start()
    {
        initialYRotation = transform.rotation.eulerAngles.y;
        initialZRotation = transform.rotation.eulerAngles.z;
    }

    void Update()
    {
        // Calculate the new rotation angle based on time and speed
        float angle = rotationSpeed * Time.deltaTime;

        if (rotatingLeft)
        {
            currentRotation -= angle;

            // If rotation reaches the maximum left, switch direction
            if (currentRotation <= -rotationAngle)
            {
                rotatingLeft = false;
            }
        }
        else
        {
            currentRotation += angle;

            // If rotation reaches the maximum right, switch direction
            if (currentRotation >= rotationAngle)
            {
                rotatingLeft = true;
            }
        }

        // Apply the rotation to the RectTransform
        transform.rotation = Quaternion.Euler(0, initialYRotation, initialZRotation + currentRotation);
    }
}
