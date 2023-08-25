using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    
    private CollectableStickman stickmanPrefab;

    
    private static int stickmanLaunchCount;
    private static float launchFrequency;
    [SerializeField] private Vector3 axisForce;
    [SerializeField] private Vector2 xForceRange;
    [SerializeField] private Vector2 yForceRange;
    [SerializeField] private Vector2 zForceRange;
    private void Awake()
    {
        stickmanPrefab = SpawnerManager.Instance.stickmanPrefab;
        stickmanLaunchCount = SpawnerManager.Instance.stickmanLaunchCount;
        launchFrequency = SpawnerManager.Instance.launchFrequency;
    }
    private void Start()
    {
        stickmanPrefab.enabled = false;
        GameManager.onCollectingPhaseEnded += StopSpawning;
        StartCoroutine(Test());
    }
    IEnumerator Test()
    {

        CollectableStickman[] createdStickmans = new CollectableStickman[stickmanLaunchCount];

        for (int i = 0; i < createdStickmans.Length; i++)
        {
            createdStickmans[i] = Instantiate(stickmanPrefab, transform.position, Quaternion.identity);
            createdStickmans[i].gameObject.SetActive(false);
        }

        foreach (CollectableStickman stickman in createdStickmans)
        {
            stickman.gameObject.SetActive(true);
            stickman.physicsController.LaunchStickman(new Vector3(GetRandom(xForceRange), GetRandom(yForceRange), GetRandom(zForceRange)));

            yield return new WaitForSeconds(launchFrequency);
        }

    }
    private void StopSpawning()
    {
        gameObject.SetActive(false);
    }

    private float GetRandom(Vector2 range)
    {
        float random = Random.Range(range.x, range.y);
        return random;
    }


}
