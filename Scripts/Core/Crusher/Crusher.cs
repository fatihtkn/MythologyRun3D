using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crusher : MonoBehaviour
{
    public Rotator[] schafts;





    private void Update()
    {
        RotateSchafts();
    }
    private void RotateSchafts()
    {
        for (int i = 0; i < schafts.Length; i++)
        {
            schafts[i].RotateShaft();
        }
    }
}
