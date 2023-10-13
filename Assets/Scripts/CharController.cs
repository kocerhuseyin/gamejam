using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    public Rigidbody Rigidbody;
    public float MovementSpeed;
    public float Gravity;
    Vector3 Direction;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            Direction = Vector3.left;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            Direction = Vector3.right;
        }

        Rigidbody.velocity = Direction * MovementSpeed * Time.deltaTime;
    }
}
