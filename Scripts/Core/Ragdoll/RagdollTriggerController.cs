using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollTriggerController : MonoBehaviour
{
    [SerializeField]private Transform parent;
    private BossController boss;

    
    private void Start()
    {
        boss = parent.GetComponent<RagdollPhysicsController>().bossController;
    }
    private void OnTriggerEnter(Collider other)
    {
        bool control = other.CompareTag("EndPoint");
        if (control)
        {
            GetComponent<Collider>().enabled = false;
            boss.OnEatStickman();
           
           
            Destroy(parent.gameObject);
        }
    }


   
}
