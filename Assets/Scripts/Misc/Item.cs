using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject {

    new public string name = "New Item";
    public Sprite icon = null;
    public bool isDefaultItem = false;
    public PlayerStats.PlayerClasses itemClass;
    public RareTypes rareType;
    public int sellPrice;
    public int buyPrice;

    [HideInInspector]
    private bool equipState = false;

    public bool EquipState
    {
        get
        {
            return equipState;
        }

        set
        {
            equipState = value;
        }
    }

    public virtual void Use()
    {
        Debug.Log("Using item: " + name);
    }

    public virtual int GetPower()
    {
        //Will be filled in weapon class
        return 0;
    }

    public virtual Item GetHigher()
    {
        return this;
    }

    public void RemoveFromInventory()
    {
        Inventory.instance.DeleteItem(this);
    }

    public enum RareTypes { Common, Uncommon, Rare, Legendary }
}
