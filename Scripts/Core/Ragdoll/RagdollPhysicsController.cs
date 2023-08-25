using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPhysicsController : MonoBehaviour, Pullable
{
    public bool isPullable { get; set; }
    [HideInInspector] public BossController bossController;
    private Vector3 target;
    public Rigidbody rb;
    [Header("Pull")]
    public float pullForce=9000f;
    private void Start()
    {
        pullForce = 15000f;
        GameManager.onCollectingPhaseEnded += DestroyAllActiveRagdolls;
       
    }
    private void FixedUpdate()
    {
        if(isPullable&rb!=null)
        {
            Vector3 direction = GetPullDirection();
            float fixedDelta = Time.fixedDeltaTime;
            rb.AddForce(direction * (pullForce * fixedDelta*15));
            rb.AddTorque(new Vector3(1, 0, 1) * pullForce * 11 * fixedDelta);
        }
       
        
    }
    private Vector3 GetPullDirection()
    {
        Vector3 pullDirection = (target - rb.position).normalized;
        return pullDirection;
    }
   
    public void Initalize(Vector3 target)
    {
        this.target = target;
        isPullable = true;
    }

    private void OnDestroy()
    {
        GameManager.onCollectingPhaseEnded -= DestroyAllActiveRagdolls;
    }

    private void DestroyAllActiveRagdolls()
    {
        Destroy(gameObject);
    }
   

}
