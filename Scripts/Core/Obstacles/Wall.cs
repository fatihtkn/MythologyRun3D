using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    Rigidbody[] rbs;
    public Rigidbody[] rbsToCollide;
    private CustomGravity[] customGravity;
    [HideInInspector] public bool isTheWallBroken;
    private void Start()
    {
        rbs = GetComponentsInChildren<Rigidbody>();
        customGravity = new CustomGravity[rbs.Length];
        for (int i = 0; i < rbs.Length; i++)
        {
            customGravity[i] = rbs[i].GetComponent<CustomGravity>();
        }
    }

    public void SetRb()
    {
        for (int i = 0; i < rbs.Length; i++)
        {
            rbs[i].isKinematic = false;
            // rbs[i].useGravity = true;
            customGravity[i].useCustomGravity= true;    
        }
        
    }
    private void SetForce(Rigidbody rb)
    {
        rb.velocity = new Vector3(-180f, UnityEngine.Random.Range(40,60), UnityEngine.Random.Range(20,50));
    }
    public IEnumerator BreakeWall(Action onWallBreake)//Animation Event
    {
        SetRb();

       
        yield return new WaitForSeconds(0.65f);

        for (int i = 0; i < rbsToCollide.Length; i++)
        {
            SetForce(rbsToCollide[i]);
        }
        onWallBreake.Invoke();
    }
    public void DestroyWall()
    {
        gameObject.SetActive(false);
    }
}
