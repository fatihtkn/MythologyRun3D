using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    public GameObject bossMesh;
    private Pipe pipe;
    public float scaleIncreaseRate = 0.2f;
    public GameObject UIMenuObject;
    public Image progressBar;
    public TMP_Text levelText;
    public BossType currentBossType;
    float fillAmountIncreaseRate;
    int levelupThreshold = 15;
    static readonly int maxLevel = 2;
    int currentLevel=1;
    [SerializeField] private int eatenStickmanCount;
  
    [HideInInspector]public BossItemController bossItem;
    [HideInInspector] public BossAnimationController bossAnimationController;
    [HideInInspector] public PlayerBossMovement bossMovement;
    float targetFillAmount;
    public bool isfinishedTheGame;
    private void Awake()
    {
        pipe=GetComponentInChildren<Pipe>(); 
        
        currentLevel = 1;
        levelupThreshold = 15;
        fillAmountIncreaseRate = progressBar.fillAmount / 15;
        progressBar.fillAmount = 0f;
        bossItem=GetComponent<BossItemController>();
        bossAnimationController=GetComponent<BossAnimationController>();
        bossMovement=GetComponent<PlayerBossMovement>();    
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Finish"))
        {
            isfinishedTheGame = true;
            GameManager.EndGame();
        }
    }
    private IEnumerator Start()
    {
        GameManager.onCollectingPhaseEnded += OnCollectingPhaseEnded;
     

       yield return new WaitForSeconds(2f);
        if (GameManager.Instance.Running_Phase)
        {
            OnMaxLevel();
            bossItem.UnlockAllItems();
        }
    }

    public void OnEatStickman()
    {
        IncreaseScale();
        SetBarFillAmount();
        eatenStickmanCount++;
        ControlTheLevelIncrease();
    }
    private void IncreaseScale()
    {
        if (bossMesh.transform.localScale.x < 9f)
        {
            bossMesh.transform.DOScale(bossMesh.transform.localScale + (Vector3.one * scaleIncreaseRate), 0.8f);
        }
    }
    private void SetBarFillAmount()
    {
        targetFillAmount += fillAmountIncreaseRate;
        progressBar.DOFillAmount(targetFillAmount, 0.5f);
    }
    private void ResetProgressBar()
    {
        progressBar.fillAmount = 0;
        targetFillAmount = 0;
    }
    private void ControlTheLevelIncrease()
    {
        if (currentLevel < maxLevel)
        {
            if (eatenStickmanCount == levelupThreshold)
            {
                LevelUp();
            }
        }
        else
        {
            OnMaxLevel();
        }
       
    }
    private void OnMaxLevel()
    {
        GameManager.Instance.EndCollectingPhase();
    }
    private void LevelUp()
    {
        
        currentLevel++;
        levelText.text = currentLevel.ToString();
        ResetProgressBar();
        levelupThreshold += levelupThreshold;
        bossItem.UnlockItemInOrder();

    }
    private IEnumerator RotateBoss()
    {
        pipe.SetActivity(false);
        yield return new WaitForSeconds(1.8f);
        transform.DOLocalRotate(Vector3.up * 180f, 0.6f).OnComplete(() =>
        {
            GameManager.Instance.CurrentState = GameStates.Running;


        });
    
       
      
    }
    private void OnCollectingPhaseEnded()
    {
        StartCoroutine(RotateBoss());
        SetActivityUIMenu(false);
    }

    private void SetActivityUIMenu(bool control)
    {
        UIMenuObject.SetActive(control);
    }
    
}
public enum BossType
{
    Enemy,
    Player
}