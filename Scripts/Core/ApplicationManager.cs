using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApplicationManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this);
        Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        //Application.targetFrameRate = 60;
    }
}
