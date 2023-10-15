using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakasScript : MonoBehaviour
{
    public float StartTime;
    Animator Animator;

    void AnimatorStart()
    {
        Animator.Play("MakasAnim");
    }

    // Start is called before the first frame update
    void Start()
    {
        Animator = GetComponent<Animator>();
        Invoke("AnimatorStart", StartTime);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
