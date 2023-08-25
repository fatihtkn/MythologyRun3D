using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBrain : MonoBehaviour, IMovable
{
    private bool _isMoving;
    public bool moveDelta { get => _isMoving; set => _isMoving = value; }
    private NavMeshAgent agent;
    private Animator animator;
    private StickmanCollector collector;
    public Transform releasePoint;
    
    private void Awake()
    {
        collector=GetComponent<StickmanCollector>();
        animator= GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }
    private void Start()
    {
        collector.isStackLimitMax = false;
    }

    private void Update()
    {
        SetTargetStickman();
        SetRunAnimation();
        DropCollectedStickmans();
    }
    
    private CollectableStickman GetClosestStickman()
    {
        Vector3 ourPosition = transform.position;
        CollectableStickman targetStickman = null;
        float closestDistance = Mathf.Infinity;
        List<CollectableStickman> stickmans = CollectableStickmansManager.Instance.GetStickmanList();

        foreach (CollectableStickman stickman in stickmans )
        {
            float distanceDelta= Vector3.Distance(ourPosition, stickman.transform.position);
            if (distanceDelta < closestDistance)
            {
                closestDistance = distanceDelta;
                targetStickman = stickman;
            }
        }
        return targetStickman;
    }

    private void SetTargetStickman()
    {
        if (!collector.isStackLimitMax)
        {
            CollectableStickman closestStickman = GetClosestStickman();

            if (closestStickman != null)
            {
                agent.SetDestination(closestStickman.transform.position);
            }
        }
       
       
    }
    private void SetRunAnimation()
    {
        if (agent.remainingDistance <= agent.stoppingDistance)
        {
            agent.isStopped= true;
            animator.SetBool("Run", false);
            _isMoving = false;
        }
        else
        {
            agent.isStopped = false;
            animator.SetBool("Run", true);
            _isMoving= true;
        }
    }


    private void DropCollectedStickmans()
    {
        if (collector.currentStickmanCount >= 15)
        {
            collector.isStackLimitMax = true;
            agent.SetDestination(releasePoint.position);

        }
    }
}
