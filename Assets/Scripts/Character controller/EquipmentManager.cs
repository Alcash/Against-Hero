using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EquipmentManager : MonoBehaviour {

   

    Equipment[] currentEquipment;
    SkinnedMeshRenderer[] currentMeshes;

    public UnityAction<Equipment, Equipment > onEquipmentChanged;

    public Equipment[] defaultItems;
    public SkinnedMeshRenderer targetMesh;
    Inventory inventory;
    // Use this for initialization
    void Start () {
        int numSlots =  System.Enum.GetNames(typeof(EquipmentSlot)).Length;
        currentEquipment = new Equipment[numSlots];
        inventory = Inventory.instance;
        currentMeshes = new SkinnedMeshRenderer[numSlots];
        EquipDefaultItems();
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.U))
        {
            UnequipAll();
        }
	}

    public void Equip(Equipment newItem)
    {
        int slotIndex = (int)(newItem.equipSlot);
        
        Equipment oldItem = Unequip(slotIndex);      

        if(onEquipmentChanged != null)
        {
            onEquipmentChanged(newItem,oldItem);
        }
        currentEquipment[slotIndex] = newItem;

        SetEquipmentBlendShapes(newItem, 100);
        SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newItem.mesh, targetMesh.transform);
        newMesh.transform.parent = targetMesh.transform;

        newMesh.bones = targetMesh.bones;
        newMesh.rootBone = targetMesh.rootBone;
        currentMeshes[slotIndex] = newMesh;
    }

    public Equipment Unequip(int slotIndex)
    {
        if(currentEquipment[slotIndex] != null)
        {

            if(currentMeshes[slotIndex] != null)
            {
                Destroy(currentMeshes[slotIndex].gameObject);
            }
            Equipment oldItem = currentEquipment[slotIndex];
            SetEquipmentBlendShapes(oldItem, 0);

            inventory.Add(oldItem);

            currentEquipment[slotIndex] = null;
            if (onEquipmentChanged != null)
            {
                onEquipmentChanged(null, oldItem);
            }
            return oldItem;
        }
        return null;
    }

    public void UnequipAll()
    {
        for (int i = 0; i < currentEquipment.Length; i++)
        {
            Unequip(i);
        }

        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment item, int weight )
    {
        foreach (EquipmentMeshRegion blendShape in item.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)blendShape, weight);
        }
    }

    void EquipDefaultItems()
    {
        foreach (var item in defaultItems)
        {
            Equip(item);
        }
    }
}
