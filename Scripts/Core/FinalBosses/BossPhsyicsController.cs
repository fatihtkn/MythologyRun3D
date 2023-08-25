using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossPhsyicsController : MonoBehaviour
{

    BossController boss;
     Action afterWallBrake;
   
    private void Start()
    {
        boss = GetComponent<BossController>();
        afterWallBrake += AfterWallBreakeActions;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallTrigger"))
        {
            boss.bossMovement.SetMoveToRun(false);
            boss.bossItem.GetItem<Hammer>().ActivatePhysicsInterraction(true);
            boss.bossAnimationController.SetAnimation("Hit", true);

            Wall wall= other.GetComponent<Wall>();
            StartCoroutine(wall.BreakeWall(afterWallBrake));

        }
        if (other.CompareTag("Climb"))
        {
            
            boss.bossMovement.SetMoveToFly(true);
            boss.bossAnimationController.SetAnimation("Fly");
        }
    }

    private void AfterWallBreakeActions()
    {
        boss.bossMovement.SetMoveToRun(true);
        boss.bossItem.GetItem<Hammer>().ActivatePhysicsInterraction(false);
        boss.bossAnimationController.SetAnimation("Hit", false);
    }
}
