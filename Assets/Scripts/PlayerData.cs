using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "PlayerData", order = 0)]
public class PlayerData : ScriptableObject
{
    public PlayerShapeState playerShapeState = PlayerShapeState.Circle;
    public float maxWetness = 100.0f;
    public float wetness = 0.0f;
    public float movementSpeed = 5.0f;
    public float wetMultiplier = 1.0f;
    public float mass = 1.0f;
}