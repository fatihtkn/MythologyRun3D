using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CollectableStickman : StickmanBase, ICollectable
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public bool targeted;
    [HideInInspector]public CollectableStickmanBrain brain;
    MPBController mpbController;
    private float duration = 3f;
    public StickmanCollector currentCollector;
    [HideInInspector] public StickmanRagdoll ragdollController;
    [HideInInspector] public StickmanPhysicsController physicsController;
     private bool _isCollected;/// <summary>
    
   
    /// ///////////test
    /// </summary>
    public bool following;
    public bool isCollected { get => _isCollected; set => _isCollected = value; }

    private void Awake()
    {
        brain = GetComponent < CollectableStickmanBrain>();
        ragdollController=GetComponent<StickmanRagdoll>();
        animator = GetComponent<Animator>();
        mpbController = GetComponentInChildren<MPBController>();
        physicsController=GetComponent<StickmanPhysicsController>();
       
        
    }
    private void Start()
    {
        CollectableStickmansManager.Instance.AddActiveStickmanToList(this);
        GameManager.onCollectingPhaseEnded += DisableThisStickman;
    }
    private void Update()
    {
       
        UpdateStickmanTransforms();
        SwitchAnimation();
       
    }
    public override IEnumerator JoinTeam(Transform playerPos)
    {
        float timer = 0f;
        _isCollected = true;
        following = true;
        animator.SetBool("Run", true);
        
        SetCollider(false);
        while (Vector3.Distance(transform.position,GetTeamCoordinates(playerPos.position))>0.5f)
        {
            transform.position=Vector3.Lerp(transform.position,GetTeamCoordinates(playerPos.position), timer/duration);
            transform.localRotation = playerPos.rotation;
            timer += Time.deltaTime;
            yield return null;
        }
        transform.position=GetTeamCoordinates(playerPos.position);
        IsJoined= true;
        
    }
    protected override Vector3 GetTeamCoordinates(Vector3 playerPos)
    {
        float xAxis = playerPos.x + Mathf.Cos(degree * totalCollectedPieces) * radiusMultiplier;
        float zAxis = playerPos.z + Mathf.Sin(degree * totalCollectedPieces) * radiusMultiplier;

        Vector3 targetPos = new(xAxis, playerPos.y, zAxis);
        return targetPos;
    }
    public void SetPieceValue(int totalCollected)
    {
        totalCollectedPieces =totalCollected;
        UpdateMaxRadius(totalCollectedPieces);
    }
    private void Initalize()
    {
        maxStickmanLimit = currentCollector.maxStickmanLimit;
        degree = SetDegree();
        radiusMultiplier = currentCollector.radius;
        totalCollectedPieces = currentCollector.collectedStickmanCount;
    }
    private void UpdateMaxRadius(int totalCollectedPiece)
    {
        if(totalCollectedPiece == maxStickmanLimit+1)
        {
            currentCollector.IncreaseRadiusOnMaxLimit(ref radiusMultiplier);
            currentCollector.SetStickmanCollectLimit(ref maxStickmanLimit);
            currentCollector.IncreaseDegreeOnMax(ref degree, this);
            currentCollector.collectedStickmanCount = 0;
            ResetDegree();
        }
        
        
    }
    private void ResetDegree()
    {
        degree = SetDegree();
    }

    private void UpdateStickmanTransforms()
    {
        if (IsJoined&following)
        {
            Vector3 pos = GetTeamCoordinates(currentCollector.collectorTransform.position);
            Quaternion rot = currentCollector.collectorTransform.rotation;
            transform.SetPositionAndRotation(pos,rot);
        }
        

       
        
    }
    public void StartColorShift()
    {
        StartCoroutine(mpbController.StartColorShift(5f));
    }
    public void SetCollectorType(StickmanCollector collector)
    {
        currentCollector = collector;
        
       
    }
    private void SwitchAnimation()
    {
        
        if (currentCollector != null&following)
        {
            
            bool control = currentCollector.isMoving & _isCollected;
            if (control)
            {
                animator.SetBool("Run", true);
               
            }
            else
            {
                animator.SetBool("Run", false);
               
            }
        }
    }
    public void SetCollider(bool control)
    {
        //GetComponent<Collider>().enabled = control;

        Collider[] cols=GetComponents<Collider>();
        for (int i = 0; i < cols.Length; i++)
        {
            cols[i].enabled = false;
        }
    }
    public void OnCollected()
    {
        _isCollected = true;
        CollectableStickmansManager.Instance.RemoveFromActiveStickmanList(this);
        animator.SetTrigger("RunTrigger");
        brain.ResetDestinationOnJoined(CollectableStickmanStates.Collected);
        Initalize();
        
    }
    public float SetDegree()
    {
        float degree = 2f * Mathf.PI/maxStickmanLimit;
        return degree;
    }
    public void StopFollowing()
    {
        following = false;
        brain.ResetDestinationOnJoined(CollectableStickmanStates.Dropped);
        animator.SetBool("Run", false);
        
    }

    public bool ControlAvailableStickman()
    {
        
        bool control= !_isCollected & physicsController.isGrounded;
            return control;
    }
    private void OnDestroy()
    {
        GameManager.onCollectingPhaseEnded -= DisableThisStickman;
    }
    public void DisableThisStickman()
    {
        Destroy(gameObject);
    }
}
