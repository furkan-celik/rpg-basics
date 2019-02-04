using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerStats : CharacterStats {

    public PlayerClasses playerClass;

    // Use this for initialization
    void Start()
    {
        EquipmentManager.instance.onEquipmentChangeCallback += OnEquipmentChanged;
    }

    void OnEquipmentChanged(Equipment oldEquipment, Equipment newEquipment)
    {
        if (newEquipment != null)
        {
            armor.AddModifier(newEquipment.armor);
            damage.AddModifier(newEquipment.damage);
            attackCooldown.AddAffect(newEquipment.attackCooldownAffect);
        }

        if (oldEquipment != null)
        {
            armor.RemoveModifier(oldEquipment.armor);
            damage.RemoveModifier(oldEquipment.damage);
            attackCooldown.AddAffect(oldEquipment.attackCooldownAffect);
        }
    }

    public enum PlayerClasses { Assassin, Tank, Mage }
}
