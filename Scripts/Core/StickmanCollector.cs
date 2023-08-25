using System.Collections.Generic;
using UnityEngine;

public class StickmanCollector : MonoBehaviour,ICollector
{
    const string stickmanTag ="Stickman";
    public CollectorType collectorType;
    public BossController bossController;
    public int collectedStickmanCount { get; set; }
    public int currentStickmanCount;
    private IMovable movementController;
    public Transform collectorTransform => transform;
    public bool isMoving { get => movementController.moveDelta; }
    public float degree { get; set; }
    public int radius { get; set; }

    private int _maxStickmanLimit;
   [HideInInspector] public int maxStickmanLimit { get=> _maxStickmanLimit; set=> _maxStickmanLimit=value; }

   public List<CollectableStickman> collectedStickmanList=new();
   [HideInInspector] public  int stickmanLimitIncreaseRate;
   [HideInInspector] public  int radiusIncreaseRate;
    [HideInInspector] public int stickmanReleaseLimit;
    [HideInInspector]public bool isStackLimitMax;

    private void Awake()
    {
        
       stickmanLimitIncreaseRate = CollectableStickmansManager.Instance.StickmanLimitIncreaseRate;
       radiusIncreaseRate = CollectableStickmansManager.Instance.radiusIncreaseRate;
        maxStickmanLimit += stickmanLimitIncreaseRate;
        movementController = GetComponent<IMovable>();
        collectedStickmanCount = 0;
        radius = 4;
        SetStickmanReleaseLimit();
    }
    private void Start()
    {
        GameManager.onCollectingPhaseEnded += DisableThisCollector;
    }


    private void OnTriggerEnter(Collider other)
    {
        bool stickman = other.CompareTag(stickmanTag);
        if (stickman)
        {
            CollectableStickman collectableStickman = GetStickman(other);

            CollectStickman(collectableStickman);
           

        }
    }
    
    public void CollectStickman(CollectableStickman collectableStickman)
    {
        if (collectableStickman.ControlAvailableStickman())
        {
            collectableStickman.SetCollectorType(this);
            AddToCollectedStickmanList(collectableStickman);
            collectableStickman.OnCollected();
            collectableStickman.SetPieceValue(AddStickman());
            collectableStickman.StartColorShift();
            collectableStickman.SetCollider(false);
            StartCoroutine(collectableStickman.JoinTeam(collectorTransform));
            
        }
       
    }
    private CollectableStickman GetStickman(Collider other)
    {
        CollectableStickman collectableStickman = other.GetComponent<CollectableStickman>();
        return collectableStickman;
    }
    private int AddStickman()
    {   
        collectedStickmanCount++;
        currentStickmanCount++; 
        return collectedStickmanCount;
    }

    private void AddToCollectedStickmanList(CollectableStickman collectableStickman)
    {
        collectedStickmanList.Add(collectableStickman);
    }
    public void ClearCollectedStickmanList()
    {
        collectedStickmanList.Clear();
    }
    public void RemoveCollectedStickmanFromList(CollectableStickman stickman)
    {
        collectedStickmanList.Remove(stickman);
       
    }


    public void SetStickmanCollectLimit(ref int stickmanlimit)
    {
        _maxStickmanLimit += stickmanLimitIncreaseRate;
        stickmanlimit = _maxStickmanLimit;
    }
    public void IncreaseRadiusOnMaxLimit(ref int radius)
    {
        this.radius += radiusIncreaseRate;
        radius = this.radius;
    }
    public void IncreaseDegreeOnMax(ref float degree,CollectableStickman stickman)
    {
        this.degree = stickman.SetDegree();
        degree= this.degree;
    }

    private void SetStickmanReleaseLimit()
    {
        switch (collectorType)
        {
            case CollectorType.Player:
                stickmanReleaseLimit = 0;
                break;
            case CollectorType.Enemy:
                stickmanReleaseLimit= 14;
                break;
            
        }
    }
    public void ResetCircleOrderValues()
    {
        radius= 4;
        degree= 2f*Mathf.PI/15;
        collectedStickmanCount= 0;
        currentStickmanCount= 0;
        _maxStickmanLimit= 15;
        isStackLimitMax= false;

    }

    public void DisableThisCollector()
    {
        gameObject.SetActive(false);
    }
}
