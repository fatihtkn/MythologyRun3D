using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CollectableStickmanBrain : MonoBehaviour
{
    NavMeshAgent agent;
   StickmanPhysicsController physicsController;
    CollectableStickman stickman;
    float maxDistance = 100f;
    Vector3 randomPos;
   
    public CollectableStickmanStates currentState;
    

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        physicsController= GetComponent<StickmanPhysicsController>();
        randomPos = SetRandomLocation();
        stickman= GetComponent<CollectableStickman>();
        agent.enabled = false;
       
        currentState = CollectableStickmanStates.Spawning;
    }

    private void Update()
    {
        RunToRandomPos();
       
    }

    private void RunToRandomPos()
    {
        
        if (physicsController.isGrounded&currentState== CollectableStickmanStates.Running)
        {

            stickman.enabled = true;
            agent.enabled = true;
            agent.SetDestination(randomPos);
            //currentState = CollectableStickmanStates.Running;
            NotifyOnArrival();

            if (stickman.currentCollector != null)
            {
                print("BuanasýS2knedenHalaÇalýþýyor?");
            }

        }

    }
    private Vector3 SetRandomLocation()
    {
        Vector3 randomPos = new Vector3(UnityEngine.Random.Range(-maxDistance,maxDistance),0, UnityEngine.Random.Range(-maxDistance, maxDistance));
        return randomPos;
    }
    private void NotifyOnArrival()
    {
        if (physicsController.isGrounded&currentState==CollectableStickmanStates.Running)
        {
            if (!agent.pathPending&agent.remainingDistance <= agent.stoppingDistance)
            {

                ArrivedActions();
            }
        }
    }
    private void ArrivedActions()
    {
        stickman.animator.SetTrigger("WaveHand");
        agent.isStopped = true;
        ResetDestinationOnJoined(CollectableStickmanStates.Arrived);
        currentState = CollectableStickmanStates.Arrived;
    }
    public bool HasDestinationReached()
    {

        if (agent != null)
        {
            if (!agent.pathPending & agent.remainingDistance <= agent.stoppingDistance)
            {
                agent.isStopped = true;
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
        
       

    }
    public void SetDestination(Vector3 pos)
    {
        agent.destination = pos;
    }
   
    public void ResetDestinationOnJoined(CollectableStickmanStates state)
    {
        currentState = state;
        if(agent.enabled) { agent.ResetPath(); }
       

    }
    public void SetAIState(CollectableStickmanStates state)
    {
        currentState = state;
    }
    public void SetAgentSpeed(float value)
    {
        agent.speed= value;
    }

}
