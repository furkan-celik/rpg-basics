using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeUI : MonoBehaviour {

    #region singleton

    public static TradeUI instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one trade UI.");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform parentObject;
    public GameObject tradeUI;
    public GameObject inventoryUI;

    [HideInInspector]
    public InventorySO inventory;

    private InventorySlot[] inventorySlots;

    private void Start()
    {
        inventorySlots = parentObject.GetComponentsInChildren<InventorySlot>();
    }

    public void TriggerUI(InventorySO _inventory)
    {
        inventory = _inventory;
        InitializeUI();
    }

    public void UpdateUI()
    {
        InitializeUI();
    }

    private void InitializeUI()
    {
        Debug.Log("TradeUI initializing");
        for(int i = 0; i < inventory.items.Count; i++)
        {
            if (inventory.items[i].item != null && inventory.items[i].IsSaleable())
                inventorySlots[i].SetItem(inventory.items[i].item);
            else
                inventorySlots[i].ClearSlot();
        }
        tradeUI.SetActive(true);
        InventoryUI.instance.triggered = true;
    }
}
