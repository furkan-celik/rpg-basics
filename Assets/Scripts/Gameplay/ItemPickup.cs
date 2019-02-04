using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable {

    public Item item;

    public override void Interaction()
    {
        base.Interaction();

        item.EquipState = false;

        PickUp();
    }

    void PickUp()
    {
        Debug.Log("Picking up " + item.name);

        if(Inventory.instance.AddItem(item)) //adding item to inventory and than destroying item
            Destroy(gameObject);
    }
}
