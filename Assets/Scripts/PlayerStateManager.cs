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
    public PlayerShapeState currentState = PlayerShapeState.Circle;

    // These values can be tweaked in the Unity Editor to get the desired effect.
    public float circleWetMultiplier = 1.0f;
    public float planeWetMultiplier = 1.5f;
    public float boatWetMultiplier = 0.5f;

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
        // ADD STATE CHANGE ANIMATION HERE <------
    }
}
