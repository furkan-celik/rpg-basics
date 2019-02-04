using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item {

    public EquipmentSlot equipmentSlot;
    public SkinnedMeshRenderer mesh;
    public EquipmentMeshRegions[] coveredMeshRegions;
    public int armor;
    public int damage;
    public float attackCooldownAffect;
    public int durability = 100;

    public override void Use()
    {
        base.Use();

        //Equip item
        EquipmentManager.instance.Equip(this);

        //Remove from inventory
        RemoveFromInventory();
    }

    public override int GetPower()
    {
        if(equipmentSlot == EquipmentSlot.Weapon)
        {
            return damage;
        }
        else
        {
            return armor;
        }
    }

}

public enum EquipmentSlot { Head, Chest, RightArm, LeftArm, Legs, Weapon, Shield, Feet }
public enum EquipmentMeshRegions { Legs, Arms, Torso } //corresponds to body regions of player model
