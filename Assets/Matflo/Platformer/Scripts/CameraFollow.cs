using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // The target object to follow
    public float smoothSpeed = 0.125f; // The smoothness of the camera movement
    public float yOffset = 0f;

    private Vector3 velocity = Vector3.zero;
    private Vector3 desiredPosition;
    private float xOffSet;

    void Start()
    {
        xOffSet = target.position.x;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            // Calculate the desired position of the camera
            desiredPosition = new Vector3(target.position.x - xOffSet, transform.position.y, transform.position.z);
            desiredPosition.y += yOffset;
            // Smoothly move the camera towards the desired position using SmoothDamp
            Vector3 smoothedPosition = Vector3.SmoothDamp(transform.position, desiredPosition, ref velocity, smoothSpeed);
            // Set the position of the camera
            transform.position = smoothedPosition;
        }
    }
}
