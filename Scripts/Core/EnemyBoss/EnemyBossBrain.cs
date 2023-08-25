using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBossBrain : MonoBehaviour
{
   NavMeshAgent agent;
    Animator animator;

    public Transform currentTargetPos;
    public Transform[] targetTransforms;
    int index;
    BossController bossController;
    public EnemyBossStates currentState;
    public float wallBreakDownTime=2f;
    private void Start()
    {
        bossController = GetComponent<BossController>();
        agent = GetComponent<NavMeshAgent>();   
        animator = GetComponentInChildren<Animator>();
        currentTargetPos = targetTransforms[index];
        GameManager.onGameStateChanged += StartRun;
        GameManager.onGameFinished += OnReachedFinishLine;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("WallTrigger"))
        {
            Wall wall=other.GetComponent<Wall>();
            StartCoroutine(BreakDownWall(wall));
        }
       

    }

    private void Update()
    {
        switch (currentState)
        {
            case EnemyBossStates.Running:
                Run();
                break;
            case EnemyBossStates.Climbing:
                Climb();
                break;
            default:
                break;
        }
    }
    private void Run()
    {
        agent.SetDestination(currentTargetPos.position);
        
        if (HasDestinationReached())
        {
            if (index < 3)
            {
                index++;
                if (targetTransforms[index] != null) currentTargetPos = targetTransforms[index];

                currentState = EnemyBossStates.Climbing;
                agent.speed = 65f;
                agent.enabled = false;
                animator.SetTrigger("Climb");
                
            }
            //else
            //{
            //    currentTargetPos = targetTransforms[targetTransforms.Length - 1];
            //}

        }
        

    }
    private void Climb()
    {
        transform.position = Vector3.Lerp(transform.position, currentTargetPos.position,Time.deltaTime/4.5f);
        float distance = Vector3.Distance(transform.position, currentTargetPos.position);
        print(distance);
        bool hasReached=distance<=22f;
        if(hasReached)
        {
            index++;
            if (targetTransforms[index] != null) currentTargetPos = targetTransforms[index];
            agent.enabled = true;
            currentState = EnemyBossStates.Running;
            animator.SetTrigger("Run");
        }
       
    }
    private IEnumerator BreakDownWall(Wall wall)
    {
        
        agent.isStopped = true;
        animator.SetBool("Punch", true);
        currentState = EnemyBossStates.Default;
        yield return new WaitForSeconds(wallBreakDownTime);
        wall.DestroyWall();
        agent.isStopped = false;
        animator.SetBool("Punch", false);
        agent.SetDestination(currentTargetPos.position);
        currentState = EnemyBossStates.Running;

    }
   

    private bool HasDestinationReached()
    {
        bool control = !agent.pathPending & agent.remainingDistance <= agent.stoppingDistance;
        return control; 
       
    }
   private void StartRun()
   {
        currentState = EnemyBossStates.Running;
        animator.SetTrigger("Run");
        agent.updateRotation = false;
   }
    private void OnReachedFinishLine()
    {
        if (bossController.isfinishedTheGame)
        {
            agent.ResetPath();
            agent.isStopped = true;
            animator.SetTrigger("Idle");
        }
        else
        {
            Destroy(gameObject);
        }
    }

}
public enum EnemyBossStates
{
    Default,
    Running,
    Climbing
}


