using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    float timer;
    static float duration;
    [HideInInspector]public SnakeStates currentState;
    [HideInInspector]public bool canAttack;
    static float stoppingDistance;
    Vector3 targetOffset;
    Vector3 targetPosition;
    Vector3 startPos;
    [HideInInspector]public MedusaHair medusaController;
    //private Transform startPos;
    private void Awake()
    {
        canAttack = true;
        stoppingDistance = 1f;
        duration = 0.3f;
        targetOffset = new Vector3(0, 3f, 0);
    }
   

    public IEnumerator Atack(Transform targetPos,MedusaHair medusaController)
    {
        bool isSnakeArrived = false;
        timer = 0f;
        canAttack = false;
        medusaController.SetIdleAnimation(false);
        Vector3 startPos = transform.localPosition;

        currentState = SnakeStates.Atacking;

        while (!isSnakeArrived&targetPos!=null)
        {

            switch (currentState)
            {

                case SnakeStates.Atacking:
                    StartAtacking(targetPos.position, medusaController);
                    break;
                case SnakeStates.Returning:
                    Return(startPos, ref isSnakeArrived,medusaController);
                    break;
            }

            yield return null;
        }
      

    }
    private void StartAtacking(Vector3 targetPos,MedusaHair controller)
    {
        
        float distance = Vector3.Distance(transform.position, targetPos + targetOffset);
        transform.position = Vector3.Lerp(transform.position, targetPos + targetOffset, timer / duration);
       
        timer += Time.deltaTime;
       
        if (distance <= stoppingDistance)
        {
            timer = 0;
           controller.collector.CollectStickman(controller.targetStickman);
            currentState= SnakeStates.Returning;
            
           
        }
    }
    private void Return(Vector3 startPos,ref bool isSnakeArrived, MedusaHair controller)
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, timer / duration);
        float distance = Vector3.Distance(transform.localPosition,startPos);
        timer += Time.deltaTime;
        if (distance <=0.2f)
        {
            isSnakeArrived = true;
            currentState = SnakeStates.Passive;
            canAttack = true;
            timer = 0f;
            transform.localPosition = startPos;
            controller.SetIdleAnimation(true);

        }
    }



    #region Update ile
    public void StartSnakeAtack(MedusaHair hair, Transform targetPos)
    {
        medusaController = hair;
        timer = 0f;
        canAttack = false;
        medusaController.SetIdleAnimation(false);
       
        targetPosition=targetPos.position;
        startPos = transform.localPosition;
        currentState = SnakeStates.Atacking;

    }

    private void AtackU()
    {
        float distance = Vector3.Distance(transform.position, targetPosition/*+targetOffset*/);
        transform.position = Vector3.Lerp(transform.position, targetPosition/*+targetOffset*/, timer / duration);
        timer += Time.deltaTime;

        if (distance <= stoppingDistance)
        {
            timer = 0;
            medusaController.collector.CollectStickman(medusaController.targetStickman);
            currentState = SnakeStates.Returning;


        }

    }
    private void ReturnU()
    {
        transform.localPosition = Vector3.Lerp(transform.localPosition, startPos, timer / duration);
        float distance = Vector3.Distance(transform.localPosition, startPos);
        timer += Time.deltaTime;
        if (distance <= 0)
        {
            
            
            canAttack = true;
            timer = 0f;
            medusaController.SetIdleAnimation(true);
            currentState = SnakeStates.Passive;

        }
    }
    #endregion
}
