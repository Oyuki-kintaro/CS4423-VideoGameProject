using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    //public Vector3 offset; // Offset from the target object

    [SerializeField] private float smoothTime = 0.3f; // Smoothness of the camera movement
    private Vector3 velocity = Vector3.zero; // Used for smooth damping

    [SerializeField] private Vector2 minBound; // Minimum boundaries for the camera
    [SerializeField] private Vector2 maxBound; // Maximum boundaries for the camera
    [SerializeField] private Vector3 offset = new Vector3(0, 0, -10);

    //public float smoothSpeed = 5f;
    private float cameraHalfWidth;
    private float cameraHalfHeight;

    /*void FixedUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        transform.position = Vector3.Lerp(transform.position,desiredPosition,smoothSpeed*Time.deltaTime);
        //transform.position = target.transform.position + offset;
    }*/

    void Start()
    {
        // Calculate the camera's half width and height based on the orthographic size and aspect ratio
        Camera camera = GetComponent<Camera>();
        cameraHalfHeight = camera.orthographicSize;
        cameraHalfWidth = cameraHalfHeight * camera.aspect;

        Vector3 startPosition = player.position + offset;
        transform.position = startPosition;
    }

    void FixedUpdate()
    {
        if (player != null)
        {
            // Define the target position for the camera
            Vector3 targetPosition = player.position;

            // Clamp the camera's position to the boundaries
            targetPosition.x = Mathf.Clamp(targetPosition.x, minBound.x + cameraHalfWidth, maxBound.x - cameraHalfWidth);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minBound.y + cameraHalfHeight, maxBound.y - cameraHalfHeight);

            // Smoothly move the camera to the target position
            //transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
            transform.position = Vector3.Lerp(transform.position,targetPosition,smoothTime*Time.deltaTime);

            // Keep the camera's z-position constant to avoid unintended movement in the z-axis
            transform.position = new Vector3(transform.position.x, transform.position.y, -10f); // Adjust as needed
        }
    }

}
