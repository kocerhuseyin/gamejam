using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float MovementSpeed;
    public float Gravity;
    public float JumpFactor;

    Vector3 Direction;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();  
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody.AddForce(Vector3.up * JumpFactor);
        }

        Vector3 Vel = Rigidbody.velocity;

        Vel.x = Direction.x * MovementSpeed * Time.deltaTime;

        Rigidbody.velocity = Vel;
        Debug.Log(Vel);
    }
}
