using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class StickmanPhysicsController : MonoBehaviour
{
    [HideInInspector]public Rigidbody rb;
    Collider col;
    CollectableStickman stickman;
    public bool isGrounded;
    private bool isTouched = false;
    CustomGravity customGravity;
    private void Start()
    {
        stickman= GetComponent<CollectableStickman>();
        customGravity = GetComponent<CustomGravity>();
    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.CompareTag("Floor")&!isTouched)
        {
            isGrounded= true;
            rb.velocity = Vector3.zero;
            rb.useGravity = false;
            rb.isKinematic = true;
            stickman.animator.SetBool("Run", true);
            stickman.brain.SetAIState(CollectableStickmanStates.Running);
            isTouched = true;
            customGravity.UseCustomGravity(false);
        }
    }
    private void Initalize()
    {
        SetRigidbody();
    }
    private void SetRigidbody()
    {
        rb = gameObject.AddComponent<Rigidbody>();
        col = GetComponent<Collider>();
        AddCheckerCollider();
        col.isTrigger = false;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        rb.mass = 15f;
        rb.useGravity = true;
        rb.interpolation = RigidbodyInterpolation.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void ScaleAnimation()
    {
        Vector3 currentScale=transform.localScale;
        transform.DOScale(0,3f).From().SetEase(Ease.InOutQuint);
    }

    public void LaunchStickman(Vector3 desiredForce)
    {
        Initalize();
        ScaleAnimation();

        Vector3 force = desiredForce;
        rb.velocity = force;
    }
    private void AddCheckerCollider()
    {
        BoxCollider checker= gameObject.AddComponent<BoxCollider>();
        checker.size = new Vector3(1, 5, 1);
        checker.center += new Vector3(0, 2.5f, 0);
        checker.isTrigger= true;
    }



}

