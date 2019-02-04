using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour {

    #region singleton

    public static InventoryUI instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Scene has more than one InventoryUI!!!");
            return;
        }

        instance = this;
    }

    #endregion

    public Transform itemsParent;
    public GameObject inventoryUI;

    [HideInInspector]
    public bool triggered = false;

    Inventory inventory;
    InventorySlot[] inventorySlots;

	// Use this for initialization
	void Start () {
        inventory = Inventory.instance;
        inventory.onInventoryChangedCallback += UpdateUI;

        inventorySlots = itemsParent.GetComponentsInChildren<InventorySlot>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Inventory"))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
        if (triggered)
        {
            inventoryUI.SetActive(true);
            triggered = false;
        }
	}

    void UpdateUI()
    {
        Debug.Log("UPDATING INVENTORY UI");
        
        for(int i = 0; i < inventorySlots.Length; i++)
        {
            if (i < inventory.items.Count)
            {
                inventorySlots[i].SetItem(inventory.items[i]);
            }
            else
            {
                inventorySlots[i].ClearSlot();
            }
        }
    }
}
