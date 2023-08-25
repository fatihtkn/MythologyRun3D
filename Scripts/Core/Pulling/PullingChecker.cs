using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingChecker : MonoBehaviour
{
    public CollectorType allowedCollectorType;
    public Transform targetPos;
    public Transform pullPosition;
    
    private void OnTriggerEnter(Collider other)
    {

        if (other.TryGetComponent(out StickmanCollector collector))
        {
            
            bool control = allowedCollectorType == collector.collectorType;
            if (control)
            {
              
                if (collector.currentStickmanCount > collector.stickmanReleaseLimit)
                {
                    
                    collector.ResetCircleOrderValues();
                    StartCoroutine(DropCollectedStickmans(collector));

                }
                
            }
        }
    }


    private IEnumerator DropCollectedStickmans(StickmanCollector Scollector)
    {
        StickmanCollector collector = Scollector;
        List<CollectableStickman> stickmanList = new(collector.collectedStickmanList);
        
        collector.ClearCollectedStickmanList();
        for (int i = 0; i < stickmanList.Count; i++)
        {
            CollectableStickman stickman = stickmanList[i];
            stickman.StopFollowing();        
            
        }
        
        foreach (CollectableStickman stickman in stickmanList)
        {

            if (stickman != null)
            {
                StartCoroutine(stickman.ragdollController.Test(stickman, targetPos.position, pullPosition.position));
                yield return new WaitForSeconds(0.2f);
            }
           
           


            // stickman.animator.SetBool("Run", true);
            // bool arrived = false;
            // stickman.brain.SetDestination(targetPos.position);
            //stickman.brain.SetAgentSpeed(50f);

            // while (!arrived)
            // {
            //     arrived = stickman.brain.HasDestinationReached();
            //     if (arrived)
            //     {
            //         stickman.ragdollController.ActivateRagdolls(stickman,pullPosition.position);
            //     }
            //     yield return null;
            // }
           
        }
        
    }
}
