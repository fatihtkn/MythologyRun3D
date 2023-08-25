using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MedusaHair : MonoBehaviour
{
    public Snake[] snakeHeads;
   
    Animator animator;
    public Vector3 headDirOffset;
    
    private SnakeStates currentState;
    [HideInInspector]public StickmanCollector collector;
   [HideInInspector]public CollectableStickman targetStickman;
    [Range(0f,135f)]public float availableCollectAngle;
    private void Awake()
    {
        animator= GetComponent<Animator>();
        //startPos= transform.position;
        // startRot= transform.rotation;
        collector = transform.parent.GetComponent<StickmanCollector>();
        snakeHeads=GetComponentsInChildren<Snake>();
    }
    private void OnTriggerStay(Collider other)
    {
        bool control = other.CompareTag("Stickman");

        if (control)
        {
           
            targetStickman = other.GetComponent<CollectableStickman>();
            bool isStickmanInCollectArea = CheckAngle(targetStickman);
            if (targetStickman.ControlAvailableStickman() & !targetStickman.targeted& targetStickman!=null & isStickmanInCollectArea)
            {
                Snake currentSnake = null;
                for (int i = 0; i < snakeHeads.Length; i++)
                {
                    if (snakeHeads[i].canAttack)
                    {
                        currentSnake = snakeHeads[i];
                        targetStickman.targeted = true;
                        break;
                    }
                }
                //foreach (Snake snake in snakeHeads)
                //{
                //    if (snake.canAttack)
                //    {
                //        currentSnake = snake;
                //        targetStickman.targeted = true;
                //        break;

                //    }

                //}
                if(currentSnake != null)
                {
                    StartCoroutine(currentSnake.Atack(other.transform, this));
                   
                }

            }

        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    bool control = other.CompareTag("Stickman");
        
    //    if (control)
    //    {
    //        Snake currentSnake = null;
    //       targetStickman = other.GetComponent<CollectableStickman>();
    //        bool isStickmanInCollectArea = CheckAngle(targetStickman);
    //        if (targetStickman.ControlAvailableStickman()&targetStickman!=null&isStickmanInCollectArea)
    //        {
    //            foreach (Snake snake in snakeHeads)
    //            {
    //                if (snake.canAttack)
    //                {
    //                    currentSnake=snake;
    //                    break;
                      
    //                }
                   
    //            }
    //           StartCoroutine(currentSnake.Atack(other.transform, this));
    //        }
            
    //    }
    //}
    private bool CheckAngle(CollectableStickman stickman)
    {
        Vector3 direction = stickman.transform.position - transform.position;
        float angle = Vector3.Angle(direction, transform.forward);
        bool check= angle <= availableCollectAngle * 0.5f;
        return check;
       
       
    }
   
    public void SetIdleAnimation(bool control)
    {
        if (control)
        {
            animator.enabled = control;
        }
    }


   


}
