
using System;
using UnityEngine;

[CreateAssetMenu(fileName ="New Item", menuName = "Inventory/Item" )]
public class Item : ScriptableObject {

    public string slugName = "New Item";
    public Sprite icon = null;
    

    public bool isDefaultItem = false;

    public Inventory inventory;

    public virtual void Use()
    {
        Debug.Log("Use " + slugName);
    }

    public void RemoveFromInventory()
    {
        inventory.Remove(this);
        inventory = null;
    }
}
