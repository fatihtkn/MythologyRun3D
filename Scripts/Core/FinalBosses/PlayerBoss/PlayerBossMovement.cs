using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBossMovement : MonoBehaviour
{
    [SerializeField] private CanvasBasedMoveInputs MovementInputs;
    float startMousePosX;
    private bool canRun;
    public float smoothValue;
    public float speed;
    public bool CanRun {get => canRun;}
    private bool canFly;
    BossAnimationController bossAnimationController;
    BossController bossController;
    public Transform FlyTarget;
    private void Awake()
    {
        bossController = GetComponent<BossController>();
        bossAnimationController = GetComponent<BossAnimationController>();
    }
    private void Start()
    {
        GameManager.onGameStateChanged += SetMobility;
        GameManager.onGameFinished += OnReachedFinish;
    }
    private void Update()
    {
        if (canRun)
        {
            Move();
        }

    }

    private void SetMobility()
    {
        if(GameManager.Instance.CurrentState==GameStates.Running)
        {
            canRun = true;
            bossAnimationController.SetAnimation("Run");
        }
    }

    private void Move()
    {
        if(Input.GetMouseButtonDown(0))
        {
            startMousePosX = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            float dir=startMousePosX - Input.mousePosition.x;
            Vector3 targetPos = new(dir*-1,transform.position.y,transform.position.z);
            transform.position = Vector3.Lerp(transform.position,targetPos,Time.deltaTime*smoothValue);
        }
        //-48ve 42.7
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -48f, 42.7f), transform.position.y, transform.position.z);
        transform.Translate( speed* Time.deltaTime * transform.forward);
    }
    public void SetMoveToRun(bool control)
    {
        canRun=control;
        canFly=!control;
    }
    public void SetMoveToFly(bool control)
    {
        canRun = !control;
        canFly = control;
       
        transform.DOMove(FlyTarget.position, 3f)/*.SetEase(Ease.OutCubic)*/.OnComplete(() =>
        {
            canFly = false;
            canRun = true;
            bossAnimationController.SetAnimation("Run");
        });
    }
    private void OnReachedFinish()
    {
        if (bossController.isfinishedTheGame)
        {
            canRun = false;
            bossAnimationController.SetAnimation("Idle");
        }

    }

    
}
