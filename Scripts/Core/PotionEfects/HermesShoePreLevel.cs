using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HermesShoePreLevel : MonoBehaviour, IPotionEfect
{
    MeshRenderer[] shoeMeshes;
    private event Action test;

    public GameObject[] shoeObjects;
  
    private void Start()
    {
        shoeMeshes= new MeshRenderer[shoeObjects.Length];
      
        for (int i = 0; i < shoeObjects.Length; i++)
        {
            shoeMeshes[i] = shoeObjects[i].GetComponent<MeshRenderer>();
        }
        SetActivityMeshes(false);


    }


    public void PotionEfect()
    {

        SetActivityMeshes(true);
        ScaleAnimation(test);

    }

    private void SetActivityMeshes(bool control)
    {
        for (int i = 0; i < shoeMeshes.Length; i++)
        {
            shoeMeshes[i].enabled = control;

        }
    }
    private void ScaleAnimation(Action completeActions)
    {
        shoeObjects[0].transform.DOScale(0, 1f).From().OnComplete(() => { });


        shoeObjects[1].transform.DOScale(0, 1f).From().OnComplete(() => { });
        //for (int i = 0; i < shoeObjects.Length; i++)
        //{

        //    shoeObjects[i].transform.DOScale(0, 1f).From().OnComplete(() => { });


        //}
    }
}
