using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    BoxCollider TriggerBox;
    public Animator DoorAnimator;

    void Start()
    {
        TriggerBox = GetComponent<BoxCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        DoorAnimator.Play("Open");
    }

    void Update()
    {
        
    }
}
