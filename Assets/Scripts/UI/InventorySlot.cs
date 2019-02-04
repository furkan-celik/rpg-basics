using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{

    public Image icon;
    public Button removeButton;
    public Image removeButtonImage;
    public Scrollbar scrollbar;

    Item item;
    GameObject hoverObject;
    GameObject[] hoverObjects;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetItem(Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;

        if (removeButton != null)
        {
            removeButton.interactable = true;
            removeButtonImage.enabled = true;
        }

        if(scrollbar != null)
        {
            if(item.GetType() == typeof(Equipment) || item.GetType() == typeof(Weapon))
            {
                scrollbar.enabled = true;
                scrollbar.size = ((Equipment)item).durability / 100f;
            }
        }
    }

    public void ClearSlot()
    {
        item = null;

        icon.sprite = null;
        icon.enabled = false;

        if (removeButton != null)
        {
            removeButton.interactable = false;
            removeButtonImage.enabled = false;
        }

        if(scrollbar != null)
        {
            scrollbar.enabled = false;
        }
    }

    public void DeleteItem()
    {
        if (item != null)
            Inventory.instance.DeleteItem(item);
    }

    public void UseItem()
    {
        if (item != null)
        {
            if (TradeUI.instance.tradeUI.activeSelf)
            {
                //Sell item
                CreateHoverUI.instance.CreateBargain(item, item.sellPrice);
            }
            else
            {
                item.Use();
            }
        }
    }

    public void BuyItem()
    {
        if (PlayerManager.instance.player.GetComponent<PlayerStats>().money >= item.buyPrice)
        {
            CreateHoverUI.instance.CreateBargain(item, item.buyPrice);
        }
        else
        {
            Debug.Log("Not enough money to buy " + item.name + " from " + TradeUI.instance.inventory.name + ". Player's money: " + PlayerManager.instance.player.GetComponent<PlayerStats>().money + ", Item's price: " + item.buyPrice);
        }
    }

    public void ShowItemName()
    {
        if (item != null && hoverObject == null)
        {
            if ((item.GetType() == typeof(Equipment) || item.GetType() == typeof(Weapon)) && !EquipmentManager.instance.HasAssigned((Equipment)item))
            {
                hoverObject = CreateHoverUI.instance.CreateEquipmentHoverUI(item.name, item.icon, item.GetPower(), item.itemClass, item.rareType, item.sellPrice, item.buyPrice, item.EquipState, gameObject.GetComponentInParent<Transform>());
            }
            else if (item.GetType() == typeof(Item))
                hoverObject = CreateHoverUI.instance.CreateEquipmentHoverUI(item.name, item.icon, item.GetPower(), item.itemClass, item.rareType, item.sellPrice, item.buyPrice, item.EquipState, gameObject.GetComponentInParent<Transform>());
            else
            {
                hoverObjects = CreateHoverUI.instance.compareTwoItems(item, (Item)EquipmentManager.instance.GetAt(((Equipment)item).equipmentSlot), gameObject.GetComponentInParent<Transform>());
            }
        }
    }

    public void HideItemName()
    {
        if (hoverObject != null)
        {
            Destroy(hoverObject);
            hoverObject = null;
        }
        else if (hoverObjects != null)
        {
            Destroy(hoverObjects[0]);
            Destroy(hoverObjects[1]);
            hoverObjects = null;
        }
    }
}
