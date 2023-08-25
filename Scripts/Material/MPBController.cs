using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class MPBController : MonoBehaviour
{
    Renderer _rend;
    Color mainColor;
    private MaterialPropertyBlock materialPBlock;

    [Header("Color Values")]
    [SerializeField] Color targetColor;
    

    float timer;

    private void Awake()
    {
        _rend= GetComponent<Renderer>();

        materialPBlock = new MaterialPropertyBlock();
        mainColor = _rend.material.color;
    }


    //public async Task StartColorShift(float duration)
    //{
    //    float timer = 0f;
    //      float timeDelta= Time.deltaTime;
    //    while (timer < duration)
    //    {
    //        timer += deltaTime;
    //        mainColor = Color.Lerp(mainColor, targetColor, timer / duration);


    //        materialPBlock.SetColor("_Color", mainColor);
    //        _rend.SetPropertyBlock(materialPBlock);
    //        await Task.Yield();
          
            
    //    }

    //}
    public IEnumerator StartColorShift(float duration)
    {

        float timer = 0f;

        while (timer < duration)
        {
            mainColor = Color.Lerp(mainColor, targetColor, timer / duration);
            materialPBlock.SetColor("_Color", mainColor);
            _rend.SetPropertyBlock(materialPBlock);
            timer += Time.deltaTime;
            yield return null;

        }
       
    }
}
