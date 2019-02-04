using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Inventory/Weapon")]
public class Weapon : Equipment {

	public enum WeaponTypes { Melee, Wand, Ranged }
    public WeaponTypes wtype;
    public float range;

    public override void Use()
    {
        base.Use();

        EquipmentManager.instance.currentWeapon = this;
    }

    public void Attack()
    {

    }
}
