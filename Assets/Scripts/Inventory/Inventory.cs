using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour {

   #region Singleton
    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("more than 1 instance inventory");
            return;
        }
        instance = this;
    }
    #endregion

   

    public UnityAction onItemChangedCallback;

    public List<Item> items = new List<Item>();

    public int space = 20;
    
    private void Start()
    {
        
    }

    public bool Add(Item item)
    {
        if (!item.isDefaultItem)
        { if (items.Count >= space)
            {
                Debug.Log("not enaugh room");
                return false;
            }
            item.inventory = this;
            items.Add(item);
            if(onItemChangedCallback != null)
                onItemChangedCallback();
        return true;
        }
        return false;
    }

    public void Remove(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
            onItemChangedCallback();
    }
}
