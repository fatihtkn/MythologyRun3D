using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossItemController : MonoBehaviour
{
    public Item[] items;
    int row=0;

    
    private void Start()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].gameObject.SetActive(false);
            }
        }

        GameManager.onCollectingPhaseEnded += UnlockAllItems;
    }
    public void UnlockItemInOrder()
    {
        if (row<items.Length)
        {
          
            items[row].gameObject.SetActive(true);
            row++;
        }
       
    }
    public void UnlockAllItems()
    {
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] != null)
            {
                items[i].gameObject.SetActive(true);
            }
            //items[i].gameObject.SetActive(true);
        }
    }

    public T GetItem<T>() where T : Item
    {
        T selectedItem=null;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] is T)
            {
                selectedItem = (T)items[i];
                break; 
            }
        }
        return selectedItem;
    }

}
