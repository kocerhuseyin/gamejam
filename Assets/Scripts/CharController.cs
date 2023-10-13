using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharController : MonoBehaviour
{
    Rigidbody Rigidbody;
    SphereCollider SphereCollider;

    public float MovementSpeed;
    public float Gravity;
    public float JumpFactor;
    public float GroundCheckDistance;
    float CurrentSpeed;
    public bool Grounded;
    Vector3 Direction;

    int PlayerLayer;
    void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SphereCollider = GetComponent<SphereCollider>();

        PlayerLayer = LayerMask.NameToLayer("Player");
    }

    bool IsGrounded()
    {
        RaycastHit hit;
        var Pos = transform.position;
 
        Vector3 rayStart = Pos; // Adjust the offset to avoid self-collision
        Vector3 rayDirection = Vector3.down;

   
        if (Physics.Raycast(rayStart, rayDirection, out hit, float.MaxValue, ~PlayerLayer))
        {
            Debug.Log(hit.distance - SphereCollider.radius);
            if (hit.distance - SphereCollider.radius < GroundCheckDistance)
                return true;

        }
        return false;
    }

    private void OnDrawGizmos()
    {
        return;
        var Pos = transform.position;
        Pos.y -= SphereCollider.radius;
        Gizmos.DrawCube(Pos, Vector3.one); 
    }

    // Update is called once per frame
    void Update()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        CurrentSpeed = Horizontal * MovementSpeed;

        Vector3 Vel = Rigidbody.velocity;
        Vel.x = CurrentSpeed;
  

        Grounded = IsGrounded();
        if (IsGrounded())
        {
            Vel.y = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Rigidbody.AddForce(Vector3.up * JumpFactor, ForceMode.Impulse);
            }

        }
        Rigidbody.velocity = Vel;

       // Rigidbody.AddForce(Vector3.right * CurrentSpeed, ForceMode.Acceleration);
       // Rigidbody.AddForce(Gravity * Vector3.down, ForceMode.Force);
    }
}
