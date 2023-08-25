using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

public class DENEME : MonoBehaviour
{
    
   
    async void  Start()
    {
        await Task1();
        await Task2();
        
    }
    private IEnumerator Bir()
    {
        yield return new WaitForSeconds(1f);
    }
    private async Task Task1()
    {
        float timer = 0f;
        print("Birinci Async Metodu");
        while (timer<5f) 
        { 
            timer+= Time.deltaTime;
            await Task.Yield();
        }

        print("Bir Bitti");
        
    }

    private async Task Task2()
    {
        print("Ýkinci Async Metodu");
        await Task.Delay(1000);

    }

    
}
