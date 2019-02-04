using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    #region singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Game has more than one inventory!");
            return;
        }
        instance = this;
    }

    #endregion

    public delegate void OnInventoryChanged();
    public OnInventoryChanged onInventoryChangedCallback;

    public List<Item> items = new List<Item>();
    public int space = 20; //inventory size

    public bool AddItem(Item item) //add item to inventory
    {
        if (!item.isDefaultItem) //if item is not default item and there is a space in inventory than add item to inventory
        {
            if (items.Count >= space)
            {
                Debug.Log("Not enough room");
                CreateHoverUI.instance.CreateNotification("Not enough room in inventory");
                return false;
            }
            items.Add(item);

            if (onInventoryChangedCallback != null)
                onInventoryChangedCallback.Invoke();
            return true;
        }
        return false;
    }

    public void DeleteItem(Item item) //deleting item from inventory
    {
        items.Remove(item);

        if (onInventoryChangedCallback != null)
            onInventoryChangedCallback.Invoke();
    }
}
