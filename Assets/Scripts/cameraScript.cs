using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{


    public Transform target; // Reference to the object you want the camera to follow
    public float smoothSpeed = 0.125f; // Speed at which the camera follows the target

    void FixedUpdate()
    {
        if (target != null)
        {
            // Calculate the target position with the same y and z values as the camera
            Vector3 targetPosition = new Vector3(target.position.x, transform.position.y, transform.position.z);

            // Use Vector3.Lerp to smoothly move the camera towards the target position
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, targetPosition, smoothSpeed);

            // Set the camera's position to the smoothed position
            transform.position = smoothedPosition;
        }
    }
}
