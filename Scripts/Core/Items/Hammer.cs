using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : Item
{
    Rigidbody body;
    BoxCollider col;
    private void Start()
    {
        body = GetComponent<Rigidbody>();
        col = GetComponent<BoxCollider>();
        
        col.enabled = false;
    }
    public void ActivatePhysicsInterraction(bool control)
    {
       col.enabled = control;
       body.isKinematic = control;
    }
}
