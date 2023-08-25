using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StickmanBase :MonoBehaviour
{
    public float degree;
    protected int radiusMultiplier = 4;
    protected static  int radiusIncreaseRate = 4;
    protected int totalCollectedPieces;
    protected int maxStickmanLimit;
    /// <summary>
    /// 20 
    /// </summary>
    private bool isJoined;
    public bool IsJoined
    {
        get { return isJoined; }
        set { isJoined = value; }
    }


    protected abstract Vector3 GetTeamCoordinates(Vector3 playerPos);
    public abstract IEnumerator JoinTeam(Transform playerPos);


}
