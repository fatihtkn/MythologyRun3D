using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomGravity : MonoBehaviour
{
    public float customGravity = -60f; 
    Rigidbody rb;
    public bool useCustomGravity;
    private void Start()
    {
        if(TryGetComponent(out rb))
        {
            rb.useGravity = false;
            useCustomGravity = true;
        }
      
        
    }

    private void FixedUpdate()
    {
       
        if (useCustomGravity)
        {
            rb.AddForce(Vector3.up * customGravity, ForceMode.Acceleration); 
        }
    }
    public void UseCustomGravity(bool control)
    {
        rb.useGravity = !control;
        useCustomGravity = control;
    }
}
