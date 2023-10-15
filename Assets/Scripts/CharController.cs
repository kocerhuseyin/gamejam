using System.Collections;
using System.Collections.Generic;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

public class CharController : MonoBehaviour
{
    private Rigidbody Rigidbody;
    private SphereCollider SphereCollider;
    private MeshCollider MeshCollider;
    Animator Animator;
    PlayerStateManager PlayerStateManager;

    public GameObject LandParticle;
    public GameObject SplashParticle;

    public eventManScript eventManager;
    public float MovementSpeed;
    public float MovementSpeedBoat;
    public float jumpForce = 5.0f;
    public float jumpForceBoat = 3.0f;
    public float groundCheckDistance;
    public float jumpBufferLength = 0.2f; // Time window for the buffer in seconds. Can be adjusted.
    private int PlayerLayer;

    int Direction = 1;

    float JumpTimer;
    bool isGrounded;
    bool preIsGrounded;
    RaycastHit GroundHit;

    private void Start()
    {
        //
        eventManager = GameObject.Find("eventMan").GetComponent<eventManScript>();
        //

        Rigidbody = GetComponent<Rigidbody>();
        SphereCollider = GetComponent<SphereCollider>();
        MeshCollider =  GetComponent<MeshCollider>();
        PlayerStateManager = GetComponent<PlayerStateManager>();
        Animator = GetComponent<Animator>();   
        //rb.mass = playerData.mass;
        PlayerLayer = LayerMask.NameToLayer("Player");
    }

    void OnLand()
    {
 
        if (GroundHit.transform.gameObject.tag == "Water")
        {
            if (PlayerStateManager.currentState == PlayerShapeState.Boat)
            {
                var GO = Instantiate(SplashParticle, transform.position, transform.rotation);
                GO.transform.Translate(0, -SphereCollider.radius*0.8f, 0);
                GO.transform.localScale *= 0.7f;
                Destroy(GO, 1.0f);
            }
        }
        else
        {
     
            var GO = Instantiate(LandParticle, transform.position, transform.rotation);
            Destroy(GO, 1.0f);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag =="Water")
        {
            //var go = Instantiate(SplashParticle, transform.position, transform.rotation);
            //Destroy(go, 1.0f);
        }
        if (collision.transform.tag == "Scissors")
        {
            gameLost();
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Scissors")
        {
            gameLost();
            Destroy(gameObject);
        }
        if (other.transform.tag == "Shredder")
        {
            gameLost();
            Destroy(gameObject);
        }
        if (other.transform.tag == "Wire")
        {
            gameLost();
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Fan")
        {         
            var Fan = other.transform.parent.GetComponent<FanSpin>();
            var Distance = Vector3.Distance(other.transform.parent.position, transform.position);
            
            Rigidbody.AddForce((1-Mathf.InverseLerp(0, Fan.Area.height, Distance)) * other.transform.up * Fan.Power, ForceMode.Force);           
        }
    }

    private void Update()
    {
        //Grounded
        Vector3 rayStart = transform.position; // Adjust the offset to avoid self-collision

        Vector3 rayDirection = Vector3.down;

        if (Physics.Raycast(rayStart, rayDirection, out GroundHit, float.MaxValue, ~PlayerLayer))
        { 
            if (GroundHit.distance - SphereCollider.radius < groundCheckDistance)
            {
                if (GroundHit.transform.gameObject.tag == "Water")
                {
                    if (PlayerStateManager.currentState != PlayerShapeState.Boat)
                    {
                        gameLost();
                        Destroy(gameObject);
                    }
                }
                  isGrounded = true;
            }
            else
                isGrounded = false;
        }
         else isGrounded = false;

        if (isGrounded && ! preIsGrounded)
        {
            OnLand();
        }

        preIsGrounded = isGrounded;
        Move();
        Jump();     
    }

    public void gameLost()
    {
        eventManager.isGameLost = true;
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
                SphereCollider.enabled = true;
                MeshCollider.enabled = false;
                break;
            case PlayerShapeState.Plane:
                transform.rotation = Quaternion.identity;
                Rigidbody.freezeRotation = true;
                Rigidbody.drag = 10;
                SphereCollider.enabled = false;
                MeshCollider.enabled = true;
                MeshCollider.sharedMesh = PlayerStateManager.ObjectMesh;

                if (Direction != 0)
                {
                    var Yaw = PlayerStateManager.CurrentMeshObject.transform.localRotation.eulerAngles.y;
                    Yaw = Mathf.LerpAngle(Yaw, Direction > 0 ? 180 : 0, Time.deltaTime * 5.0f);
                    PlayerStateManager.CurrentMeshObject.transform.localRotation = Quaternion.Euler(new Vector3(0, Yaw, 0));
                    float DirectionLerp = Mathf.InverseLerp(0, 180, Yaw) * 2 - 1;
                    Rigidbody.velocity = new Vector3(!isGrounded ? DirectionLerp * MovementSpeed : 0, Rigidbody.velocity.y, Rigidbody.velocity.z);
                }

                break;
            case PlayerShapeState.Boat:
                Vector3 movementBoat;

                if (GroundHit.transform.tag == "Water")
                {
                    movementBoat = new Vector3(Horizontal, 0.0f, 0.0f) * MovementSpeedBoat;
                }
                else
                    movementBoat = Vector3.zero;

                Rigidbody.velocity = new Vector3(movementBoat.x, Rigidbody.velocity.y, movementBoat.z); 
                transform.rotation = Quaternion.identity;
                Rigidbody.freezeRotation = true; 
                Rigidbody.drag = 0; 
                SphereCollider.enabled = true; 
                MeshCollider.enabled = false;

                PlayerStateManager.CurrentMeshObject.transform.localPosition = new Vector3(0, -SphereCollider.radius, 0);
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
                        Rigidbody.AddForce(Vector3.up * jumpForceBoat, ForceMode.Impulse);
                        JumpTimer = 0;
                    }
                }
                break;
            default:
                break;
        }

    }
}
