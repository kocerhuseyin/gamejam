using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirFlow : MonoBehaviour
{
    public Vector3 pushDirection = Vector3.right; // �ti� y�n� (varsay�lan olarak ileri y�nde).
    public float pushForce = 25.0f;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Rigidbody playerRb = other.GetComponent<Rigidbody>();
            if (playerRb)
            {
                playerRb.AddForce(pushDirection.normalized * pushForce);
            }
        }
    }
}
