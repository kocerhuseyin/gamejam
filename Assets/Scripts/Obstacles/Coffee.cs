using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coffee : Obstacle
{
    protected override void HandlePlayerCollision()
    {
        playerAnimator.SetTrigger("Coffee");
        base.HandlePlayerCollision();
    }
}
