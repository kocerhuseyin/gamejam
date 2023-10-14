using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private SphereCollider SphereCollider;
    Animator Animator;
    PlayerStateManager PlayerStateManager;

    public float MovementSpeed;
    public float jumpForce = 5.0f;
    public float groundCheckDistance;
    public float jumpBufferLength = 0.2f; // Time window for the buffer in seconds. Can be adjusted.
    private int PlayerLayer;

    int Direction = 1;

    float JumpTimer;
    bool isGrounded
    {
        get
        {
            RaycastHit hit;
            Vector3 rayStart = transform.position; // Adjust the offset to avoid self-collision

            Vector3 rayDirection = Vector3.down;

            if (Physics.Raycast(rayStart, rayDirection, out hit, float.MaxValue, ~PlayerLayer))
            {
                if (hit.distance - SphereCollider.radius < groundCheckDistance)
                    return true;
            }
            return false;
        }
    }

    private void Start()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SphereCollider = GetComponent<SphereCollider>();
        PlayerStateManager = GetComponent<PlayerStateManager>();
        Animator = GetComponent<Animator>();   
        //rb.mass = playerData.mass;
        PlayerLayer = LayerMask.NameToLayer("Player");
    }

    private void Update()
    {
        Move();
        Jump();
    }

    private void Move()
    {
        float Horizontal = Input.GetAxis("Horizontal");
        float HorizontalRaw = Input.GetAxisRaw("Horizontal");

        if(HorizontalRaw != 0)
        Direction = HorizontalRaw > 0 ? 1 : -1;
        
        switch (PlayerStateManager.currentState)
        {
            case PlayerShapeState.Circle:           
                Vector3 movement = new Vector3(Horizontal, 0.0f, 0.0f) * MovementSpeed;
                Rigidbody.velocity = new Vector3(movement.x, Rigidbody.velocity.y, movement.z);
                Rigidbody.freezeRotation = false;
                Rigidbody.drag = 0;
                break;
            case PlayerShapeState.Plane:
                transform.rotation = Quaternion.identity;
                Rigidbody.freezeRotation = true;
                Rigidbody.drag = 10;
 
                if (Direction != 0)
                {
                    var Yaw = PlayerStateManager.CurrentMeshObject.transform.localRotation.eulerAngles.y;
                    Yaw = Mathf.LerpAngle(Yaw, Direction > 0 ? 180 : 0, Time.deltaTime * 5.0f);
                    PlayerStateManager.CurrentMeshObject.transform.localRotation = Quaternion.Euler(new Vector3(0, Yaw, 0));
                    float DirectionLerp = Mathf.InverseLerp(0, 180, Yaw) * 2 - 1;
                    Rigidbody.velocity = new Vector3(DirectionLerp * MovementSpeed, Rigidbody.velocity.y, Rigidbody.velocity.z); 
                }

                break;
            case PlayerShapeState.Boat:

                break;
            default:
                break;
        }
    }
 
    private void Jump()
    {
        switch (PlayerStateManager.currentState)
        {
            case PlayerShapeState.Circle:
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    JumpTimer += jumpBufferLength;  // buffer amount
                }

                if (JumpTimer > 0)
                {
                    JumpTimer -= Time.deltaTime;

                    if (isGrounded)
                    {
                        var Vel = Rigidbody.velocity;
                        Vel.y = 0;
                        Rigidbody.velocity = Vel;
                        Rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                        JumpTimer = 0;
                    }
                }
                break;
            case PlayerShapeState.Plane:
                break;
            case PlayerShapeState.Boat:
                break;
            default:
                break;
        }

    }
}
