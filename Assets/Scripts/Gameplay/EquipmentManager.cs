using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{

    #region singleton

    public static EquipmentManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Game has more than one equiment manager!");
            return;
        }

        instance = this;
    }

    #endregion

    public delegate void OnEquipmentChange(Equipment oldEquipment, Equipment newEquipment); //callback for equipment changes
    public OnEquipmentChange onEquipmentChangeCallback;

    public delegate void OnEquipmentSetChange(); //calback for any of the equipment change, add or delete
    public OnEquipmentSetChange onEquipmentSetChangeCallback;

    public Equipment[] currentEquipments; //equiped equipments
    public List<Equipment> defaultEquipments; //default equipments
    public SkinnedMeshRenderer[] currentMeshes; //meshes of equiped equipments
    public SkinnedMeshRenderer targetMesh; //player's mesh
    public Weapon currentWeapon; //equiped main hand weapon

    // Use this for initialization
    void Start()
    {
        int numOfSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; //initializing lists and arrays
        currentEquipments = new Equipment[numOfSlots];
        currentMeshes = new SkinnedMeshRenderer[numOfSlots];
        EquipDefaultItems(); //to quip default items
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
            UnEquipAll();
    }

    public void Equip(Equipment newEquipment)
    {
        if (newEquipment == null) //checking if newEquipment is null
        {
            Debug.LogError("New equipment is null, exiting from EquipmentManager/Equip");
            return;
        }
        
        if(newEquipment.itemClass != PlayerManager.instance.player.GetComponent<PlayerStats>().playerClass && !newEquipment.isDefaultItem) //checking if equipment's class matches to player's class
        {
            Debug.Log("Class mismatch!!! Item class type is " + newEquipment.itemClass + " player's class is " + PlayerManager.instance.player.GetComponent<PlayerStats>().playerClass);
            Inventory.instance.AddItem(newEquipment);
            return;
        }

        int slotIndex = (int)newEquipment.equipmentSlot; //calculating slot index according to quipment slot for putting equipment to array
        if (currentEquipments[slotIndex] == null) //if the quipment slot is empty, assigning newEquipment to slot
        {
            newEquipment.EquipState = true;
            currentEquipments[slotIndex] = newEquipment;

            if (onEquipmentChangeCallback != null)
            {
                onEquipmentChangeCallback.Invoke(null, newEquipment); //old, new
            }
        }
        else
        {
            Inventory.instance.AddItem(currentEquipments[slotIndex]); //if slot is not empty than first add old item to inventory and than replace it with new one
            if (onEquipmentChangeCallback != null)
            {
                onEquipmentChangeCallback.Invoke(currentEquipments[slotIndex], newEquipment); //old, new
            }

            if (currentEquipments[slotIndex].isDefaultItem)
                Destroy(currentMeshes[slotIndex].gameObject); //destroying object mesh if it is default item

            currentEquipments[slotIndex].EquipState = false; //setting equip state of old object to false and new one to true. This is essential for hover UI
            newEquipment.EquipState = true;

            currentEquipments[slotIndex] = newEquipment;
        }

        if (newEquipment.mesh != null) //if equipment has a mesh than assigning it to player model
        {
            SkinnedMeshRenderer newMesh = Instantiate<SkinnedMeshRenderer>(newEquipment.mesh);
            newMesh.transform.parent = targetMesh.transform;
            newMesh.bones = targetMesh.bones;
            newMesh.rootBone = targetMesh.rootBone;
            currentMeshes[slotIndex] = newMesh;
            SetEquipmentBlendShapes(newEquipment, 100);
        }

        onEquipmentSetChangeCallback.Invoke();
    }

    public void UnEquip(int slotIndex) //unequiping equipment in given slot
    {
        if (currentEquipments[slotIndex] != null)
        {
            if (currentMeshes[slotIndex] != null) //if mesh is avaliable than destroy it and reverse blend shape
            {
                SetEquipmentBlendShapes(currentEquipments[slotIndex], 0);
                Destroy(currentMeshes[slotIndex].gameObject);
                currentMeshes[slotIndex] = null;
            }

            currentEquipments[slotIndex].EquipState = false; //making equipstate of equipment false

            Inventory.instance.AddItem(currentEquipments[slotIndex]); //adding item to inventory
            if (onEquipmentChangeCallback != null)
            {
                onEquipmentChangeCallback.Invoke(currentEquipments[slotIndex], defaultEquipments[slotIndex]); //calling dependent functions with unequiped item and default item
            }
            currentEquipments[slotIndex] = null; //clearing equipment slot
            onEquipmentSetChangeCallback.Invoke();

            if (defaultEquipments[slotIndex] != null)
            {
                Equip(defaultEquipments[slotIndex]); //equiping default item
            }
        }
    }

    public void UnEquip(Equipment equipment)
    {
        int slotIndex = (int)equipment.equipmentSlot; //converting equipment.equipmentSlot to int and call main UnEquip function
        UnEquip(slotIndex);
    }

    public void UnEquip(EquipmentSlot equipmentSlot)
    {
        int slotIndex = (int)equipmentSlot; //converting equipmentSlot to int and call main UnEquip function
        UnEquip(slotIndex);
    }

    public void UnEquipAll() //calling UnEquip function for all items
    {
        for (int i = 0; i < currentEquipments.Length; i++)
        {
            UnEquip(i);
        }
        EquipDefaultItems();
    }

    void SetEquipmentBlendShapes(Equipment equipment, int weight) //setting blend shape of body to prevent intersections with body and equipments
    {
        foreach (EquipmentMeshRegions meshRegion in equipment.coveredMeshRegions)
        {
            targetMesh.SetBlendShapeWeight((int)meshRegion, weight);
        }
    }

    void EquipDefaultItems() //Equiping all default items. This will be called at the start function
    {
        foreach (Equipment equipment in defaultEquipments)
        {
            if (equipment != null)
            {
                Equip(equipment);
            }
        }
    }

    public bool HasAssigned(Equipment equipment) //returning if the given equioment is equiped or not
    {
        if (equipment == null && equipment.GetType() != typeof(Equipment)) //if given parameter is not equipment or null return false
            return false;

        foreach(var item in currentEquipments)
        {
            if (item != null && item.equipmentSlot == equipment.equipmentSlot && !defaultEquipments.Contains(item))
                return true;
        }
        return false;
    }

    public Equipment GetAt(EquipmentSlot slot) //returning eqipment in given slot
    {
        return currentEquipments[(int)slot];
    }

    public void GetDamage(int dice) //taking damage to equipment and decreasing it's durability
    {
        if(dice > 15) //if dice if over 15 than give damage to all items beside weapons
        {
            for (int i = 0; i < currentEquipments.Length; i++)
            {
                if(currentEquipments[i] != null && currentEquipments[i].equipmentSlot != EquipmentSlot.Weapon)
                {
                    currentEquipments[i].durability--;
                }
            }
        }else if(dice <= 15 && dice > 10) //if dice is between 15 and 10 than give damage tp head body and arms
        {
            for (int i = 1; i < currentEquipments.Length - 3; i++)
            {
                if (currentEquipments[i] != null)
                {
                    currentEquipments[i].durability--;
                }
            }
        }
        else //if dice is pver 15 than only give damage to chest
        {
            if (currentEquipments[(int)EquipmentSlot.Chest] != null)
            {
                currentEquipments[(int)EquipmentSlot.Chest].durability--;
            }
        }
    }

    public void Attacked() //if player attacked than give damage to weapon
    {
        if(currentWeapon != null)
        {
            currentWeapon.durability--;
        }
    }
}
