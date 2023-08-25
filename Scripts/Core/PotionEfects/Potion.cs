using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Potion : MonoBehaviour
{
    public Transform targetTransform;
    public GameObject potionObject;
    Collider col;
    public ParticleSystem particle;
    private IPotionEfect currentPotionEfect;
    Liquid liquid;
    float duration;
    float timer = 0f;
    float startFillAmount;
    float targetFillAmount = 1f;

    bool isClickable;
    float potionRotationDuration;
    float potionTransitionDuration;
    private void Start()
    {
        
        col = GetComponent<Collider>();
        liquid = GetComponentInChildren<Liquid>();
        if (potionObject != null)
        {
            currentPotionEfect = potionObject.GetComponent<IPotionEfect>();
            startFillAmount = liquid.fillAmount;
        }
        
        duration = particle.main.duration;
        potionRotationDuration = PotionManager.Instance.potionRotationDuration;
        potionTransitionDuration = PotionManager.Instance.potionTransitionDuration;
    }

    

    public void Select()
    {
        transform.DOMove(targetTransform.position, potionTransitionDuration).OnComplete(()=>
        {
            transform.DORotate(new Vector3(targetTransform.rotation.eulerAngles.x, targetTransform.rotation.eulerAngles.y, targetTransform.rotation.eulerAngles.z), potionRotationDuration).OnComplete(() =>
            {
                particle.gameObject.SetActive(true);
                StartCoroutine(PotionAction(currentPotionEfect));
                StartCoroutine(SetFillAmountOfPotion());


            });

        });
        transform.DOScale(targetTransform.localScale/2.4f, 1f);

    }

   
   
    private void OnMouseDown()
    {
        if (PotionManager.isSelectable)
        {
            PotionManager.isSelectable = false;
            
            Select();
            col.enabled = false;
        }
       
    }

    private IEnumerator PotionAction(IPotionEfect potionEfect)
    {
        yield return new WaitForSeconds(particle.main.duration+0.5f);
        PotionManager.isSelectable = true;

        potionEfect.PotionEfect();
        Destroy(gameObject);
        
        
    }
    private IEnumerator SetFillAmountOfPotion()
    {
       

        while (liquid.fillAmount < targetFillAmount)
        {
            timer += Time.deltaTime;
            liquid.fillAmount = Mathf.Lerp(startFillAmount, targetFillAmount, timer / duration);
            yield return null;
        }
        PotionManager.Instance.PotionClickCount++;
    }
}
