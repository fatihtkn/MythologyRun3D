using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableStickmansManager : MonoSingleton<CollectableStickmansManager>
{
	[HideInInspector]public List<CollectableStickman> currentCollectableStickmans;

	[Header("Properties")]
	public int StickmanLimitIncreaseRate=15;
	public int radiusIncreaseRate=4;

	public void AddActiveStickmanToList(CollectableStickman stickman)
	{
		currentCollectableStickmans.Add(stickman);
	}
	public void RemoveFromActiveStickmanList(CollectableStickman stickman)
	{
		currentCollectableStickmans.Remove(stickman);
	}
	public List<CollectableStickman> GetStickmanList()
	{
		return currentCollectableStickmans;
	}

	
}
