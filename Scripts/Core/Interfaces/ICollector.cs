using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICollector 
{
    public Transform collectorTransform { get; }
    public bool isMoving { get;  }

    public float degree { get; set; }
    public int maxStickmanLimit { get; set; }
    public int radius { get; set; }

    public int collectedStickmanCount { get; set; }

}
