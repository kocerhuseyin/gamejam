using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shredder : Obstacle
{
    protected override void HandlePlayerCollision()
    {
        playerAnimator.SetTrigger("Shredded");
        base.HandlePlayerCollision();
    }
}
