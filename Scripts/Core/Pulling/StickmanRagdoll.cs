using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickmanRagdoll : MonoBehaviour
{
    public GameObject ragdollPrefab;
    private CollectableStickman stickman;
    [HideInInspector] public bool isPullable;
    private RagdollPhysicsController ragdollPhysics;
   
    private void Awake()
    {
        stickman = GetComponent<CollectableStickman>();
      
        
    }
    public void ActivateRagdolls(CollectableStickman stickman,Vector3 pullPos)
    {
       GameObject go= Instantiate(ragdollPrefab,transform.position,Quaternion.identity);
        if(go.TryGetComponent(out RagdollPhysicsController activator))
        {
            activator.Initalize(pullPos);
        }
        
        Destroy(stickman.gameObject);
    }


    public IEnumerator Test(CollectableStickman stickman,Vector3 targetPos, Vector3 pullPosition)
    {

        stickman.animator.SetBool("Run", true);
        bool arrived = false;
        stickman.brain.SetDestination(targetPos);
        stickman.brain.SetAgentSpeed(50f);
       
        while (!arrived)
        {
            arrived = stickman.brain.HasDestinationReached();
            if (arrived)
            {
                GameObject ragdoll = Instantiate(ragdollPrefab, transform.position, Quaternion.identity);
                ragdollPhysics =ragdoll.GetComponent<RagdollPhysicsController>();
                ragdollPhysics.bossController = stickman.currentCollector.bossController;
                ragdollPhysics.Initalize(pullPosition);
                Destroy(stickman.gameObject);
            }

            yield return null;
        }



    }
}
