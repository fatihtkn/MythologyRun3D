using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerManager :MonoSingleton<SpawnerManager>
{
    public int stickmanLaunchCount = 25;
    [Tooltip("It demonstrates how many stickman will spawn in a second")]
    public float launchFrequency = 3;
    public CollectableStickman stickmanPrefab;
}
