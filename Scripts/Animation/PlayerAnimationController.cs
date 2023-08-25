using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoSingleton<PlayerAnimationController>
{
    float blend;
    Animator animator;
    private void Awake()
    {
        animator= GetComponent<Animator>();
    }


    public void SetMoveBlend(bool isPlayerMoving)
    {
        blend = Mathf.Clamp(blend, 0, 1);
        if (isPlayerMoving)
        {
            blend += Time.deltaTime * 3.5f;
            animator.SetFloat("blend", blend);

        }
        else
        {

            blend -= Time.deltaTime * 3.5f;
            animator.SetFloat("blend", blend);
        }
    }
}
