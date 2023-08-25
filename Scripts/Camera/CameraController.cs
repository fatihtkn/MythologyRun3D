using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CameraController :MonoSingleton<CameraController>
{

    public Vector3 distance;
    public Transform target;
    public Transform bossPos;
    public float smoothValue=1f;
    [Header("VirtualCameras")]
    public GameObject cameraOverHead;
    public GameObject bossCam;
    public GameObject currentCamera;
    
    private void Start()
    {

        SetCameraType(CameraPresets.OverHead);
        GameManager.onCollectingPhaseEnded += SetCamTypeToBoss;
        GameManager.onGameStateChanged += SetCameraOrientToPlayer;
    }
    private void LateUpdate()
    {
            currentCamera.transform.position = Vector3.Lerp(currentCamera.transform.position, target.position+distance, Time.deltaTime*smoothValue);
    }
    public void SetCameraType(CameraPresets preset)
    {
        switch (preset)
        {
            case CameraPresets.OverHead:
                currentCamera = cameraOverHead;
                break;
            case CameraPresets.Boss:
                currentCamera=bossCam;
               
                break;
            
        }
    }
    private void SetCamTypeToBoss()
    {
        target = bossPos;
        cameraOverHead.SetActive(false);
        SetCameraType(CameraPresets.Boss);
        distance = new Vector3(27, 151.5f, -100.03f);
        transform.rotation = Quaternion.Euler(13.9f,0,0);
       // bossCam.transform.eulerAngles = new Vector3(13.9f, 0, 0);
       // currentCamera.transform.eulerAngles = new Vector3(13.9f, 0, 0);
    }
    private void SetCameraOrientToPlayer()
    {
        StartCoroutine(SmoothValueShift());
    }
    private IEnumerator SmoothValueShift()
    {
        float timer = 0f;
        Vector3 target= new Vector3(0, 104.7f, -115.1f);
       
        while (Vector3.Distance(distance,target)>=0f)
        {
            distance = Vector3.Lerp(distance, target, timer / 2f);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(4.9f, 0f, 0f), timer / 1f);
            timer += Time.deltaTime;
            yield return null;
        }
        //distance = new Vector3(0, 104.7f, -115.1f);
        transform.rotation = Quaternion.Euler(4.9f, 0f, 0f);
    }
   
}
