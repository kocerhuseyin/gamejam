using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerShapeState
{
    Circle,
    Plane,
    Boat
}

public class PlayerStateManager : MonoBehaviour
{
    public GameObject TransformParticle;
    public PlayerShapeState currentState { get; set;  } = PlayerShapeState.Circle;
    public GameObject CurrentMeshObject { get; private set; }
    public Animator ObjectAnimator { get; private set; }

    public Mesh ObjectMesh{ get; private set; }

    // These values can be tweaked in the Unity Editor to get the desired effect.
    public float circleWetMultiplier = 1.0f;
    public float planeWetMultiplier = 1.5f;
    public float boatWetMultiplier = 0.5f;

    public GameObject Circle;
    public GameObject Boat;
    public GameObject Plane;


    MeshFilter MeshFilter;
    CharController CharController;

    public float CurrentWetMultiplier
    {
        get
        {
            switch (currentState)
            {
                case PlayerShapeState.Circle:
                
                    //set circle prefab active
                    //set plane inactive
                    //set boat inactive
                    return circleWetMultiplier;
                case PlayerShapeState.Plane:
                    //set circle prefab active
                    //set plane inactive
                    //set boat inactive
                    return planeWetMultiplier;
                case PlayerShapeState.Boat:
                    //set circle prefab active
                    //set plane inactive
                    //set boat inactive
                    return boatWetMultiplier;
                default:
                    return 1.0f;
            }
        }
    }

    public void ChangeState(PlayerShapeState newState)
    {
        currentState = newState;
        Destroy(CurrentMeshObject);

        switch (currentState)
        {
            case PlayerShapeState.Circle:
                CurrentMeshObject = Instantiate(Circle, this.transform);
                break;
            case PlayerShapeState.Plane:
                CurrentMeshObject = Instantiate(Plane, this.transform);
                break;
            case PlayerShapeState.Boat:
                CurrentMeshObject = Instantiate(Boat, this.transform);
                break;
            default:
                break;
        }

        Instantiate(TransformParticle, this.transform);
        ObjectAnimator = CurrentMeshObject.GetComponentInChildren<Animator>();
        ObjectMesh = CurrentMeshObject.GetComponentInChildren<MeshFilter>().sharedMesh;
        CurrentMeshObject.transform.localPosition = Vector3.zero;
        // ADD STATE CHANGE ANIMATION HERE <------
    }

    private void Start()
    {
        MeshFilter = GetComponent<MeshFilter>();
        CharController = GetComponent<CharController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeState(PlayerShapeState.Circle);
        }
        if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeState(PlayerShapeState.Plane);
        }
        if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeState(PlayerShapeState.Boat);
        }
    }
}