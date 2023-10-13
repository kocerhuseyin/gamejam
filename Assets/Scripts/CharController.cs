using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody Rigidbody;
    public float MovementSpeed;
    public float Gravity;
    public float JumpFactor;
    float CurrentSpeed;
    
    Vector3 Direction;

    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();  
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        CurrentSpeed = Horizontal * MovementSpeed;
 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Rigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
        }

        Vector3 Vel = Rigidbody.velocity;
        Vel.x = CurrentSpeed;
        Rigidbody.velocity = Vel;

        //Rigidbody.AddForce(Vector3.right * CurrentSpeed, ForceMode.Acceleration);
        Rigidbody.AddForce(Gravity * Vector3.down, ForceMode.Force);
    }
}
