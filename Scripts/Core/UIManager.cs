using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoSingleton<UIManager>
{
    public RectTransform levelFinishCanvas;

    int sayi;
    private void Start()
    {
        levelFinishCanvas.gameObject.SetActive(false);
        GameManager.onGameFinished += Action;
    }





    private void Action()
    {
        sayi++;
        if (sayi < 2)
        {
            StartCoroutine(LevelFinishedCanvasAnim());
        }
      
    }
    private IEnumerator LevelFinishedCanvasAnim()
    {
        levelFinishCanvas.gameObject.SetActive(true);
        yield return new WaitForSeconds(1f);
        
        levelFinishCanvas.DOScale(0f, 1f).From();

    }
}
