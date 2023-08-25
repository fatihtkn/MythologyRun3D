using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PotionManager : MonoSingleton<PotionManager>  
{
    public static bool isSelectable;
    private int potionClickCount;
    public float potionRotationDuration;
    public float potionTransitionDuration;
    public int PotionClickCount
    {
        get { return potionClickCount; }
        set
        {
            potionClickCount = value;
            print("pot");
            if (potionClickCount == 2)
            {
               
                PreLevelManager.Instance.playButton.SetActive(true);
            }
        }
    }
    private void Start()
    {
        isSelectable = true;
    }

}
