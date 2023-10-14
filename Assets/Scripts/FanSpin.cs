using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FanSpin : MonoBehaviour
{
    public float FanSpeed;
    public float Power;
    Transform Rotor;
    public CapsuleCollider Area { get; private set; }

    void Start()
    {
        Rotor = transform.Find("Rotor").transform;
        Area = transform.Find("Area").GetComponent<CapsuleCollider>();
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Rotor.transform.Rotate(0, 0, FanSpeed);
    }
}
