using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MedusaPreLevel : MonoBehaviour, IPotionEfect
{
    SkinnedMeshRenderer sRenderer;
    Animator animator;
    public GameObject potionObject;
    private void Start()
    {
        sRenderer=GetComponentInChildren<SkinnedMeshRenderer>();
        animator=GetComponent<Animator>();
        animator.enabled = false;
        sRenderer.enabled = false;
    }


    public void PotionEfect()
    {

        sRenderer.enabled = true;
        transform.parent.DOScale(0, 1f).From().OnComplete(() =>
        {
            animator.enabled = true; 
            //potionObject.SetActive(false);
        });

    }
}
