using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/Inventory")]
public class InventorySO : ScriptableObject
{

    public List<Item> itemList = new List<Item>(20);

    [Serializable]
    public class SaleItem
    {
        public Item item;
        private bool openToSale = true;

        public SaleItem(Item item)
        {
            this.item = item;
        }

        public void CloseToSale()
        {
            openToSale = false;
            Debug.Log(item.name + " closed for sale.");
        }

        public void OpenToSale()
        {
            openToSale = true;
            if (item != null)
                Debug.Log(item.name + " opened for sale.");
        }

        public bool IsSaleable()
        {
            return openToSale;
        }

        public void DeleteItem()
        {
            item = null;
        }
    }

    public List<SaleItem> items = new List<SaleItem>(20);

    public void Initialize()
    {
        if (items == null) items = new List<SaleItem>(20);
        Debug.Log("Initializing items list: " + name);
        for (int i = 0; i < itemList.Count; i++)
        {
            items[i] = new SaleItem(itemList[i]);
        }
    }

    public void AddItem(Item newItem)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == null)
            {
                items[i] = new SaleItem(newItem);
                return;
            }
        }
        Item lowest = items[0].item;
        for (int i = 0; i < items.Count; i++)
        {
            if (lowest.buyPrice > items[i].item.buyPrice)
                lowest = items[i].item;
        }
        int index = FindNewIndex(lowest);
        if (index >= 0)
            items[index] = new SaleItem(lowest);
    }

    public void DeleteItem(Item item)
    {
        int index = FindNewIndex(item);
        if (index >= 0)
            items[index].DeleteItem();
    }

    public void CloseItemToSale(Item item)
    {
        int index = FindNewIndex(item);
        if (index >= 0)
            items[index].CloseToSale();
    }

    public void OpenItemToSale(Item item)
    {
        int index = FindNewIndex(item);
        if (index >= 0)
            items[index].OpenToSale();
    }

    int FindNewIndex(Item item)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].item == item)
                return i;
        }
        return -1;
    }
}