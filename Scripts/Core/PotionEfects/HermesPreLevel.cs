using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class HermesPreLevel : MonoBehaviour,IPotionEfect
{
    
    public void PotionEfect()
    {

        transform.DOScale(0, 2f).From();
    }
}
