using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipSlot;
    public SkinnedMeshRenderer mesh = null;
    public EquipmentMeshRegion[] coveredMeshRegions;

    public int armorModifier;
    public int damageModifier;

    EquipmentManager equipmentManager;

    public override void Use()
    {
        base.Use();
        equipmentManager.Equip(this);
        RemoveFromInventory();

    }
}

public enum EquipmentSlot
{
    Head,
    Chest,
    Legs,
    Weapon,
    Shield,
    Feet
}
public enum EquipmentMeshRegion { Legs, Arms, Torso}; // Corresponds to body blendshapes