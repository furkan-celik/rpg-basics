using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentsUI : MonoBehaviour {

    public Transform equipmentsSlotsParent;
    public GameObject equipmentUI;
    public Sprite[] aSprite;
    public Sprite[] tSprite;
    public Sprite[] mSprite;

    Sprite[] bgSprites;
    EquipmentSlots[] equipmentSlots;

	// Use this for initialization
	void Start () {
        EquipmentManager.instance.onEquipmentSetChangeCallback += UpdateUI;
        equipmentSlots = equipmentsSlotsParent.GetComponentsInChildren<EquipmentSlots>();

        switch ((int)PlayerManager.instance.player.GetComponent<PlayerStats>().playerClass)
        {
            case 0:
                bgSprites = aSprite;
                break;
            case 1:
                bgSprites = tSprite;
                break;
            case 2:
                bgSprites = mSprite;
                break;
            default:
                bgSprites = tSprite;
                break;
        }

        InitializeUI();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Equipments"))
        {
            equipmentUI.SetActive(!equipmentUI.activeSelf);
        }
	}

    public void InitializeUI()
    {
        Debug.Log("INITIALIZING EQUIPMENT UI");

        for(int i = 0; i < bgSprites.Length; i++)
        {
            if(i < equipmentSlots.Length)
                equipmentSlots[i].SetBgSprite(bgSprites[i]);
        }
    }

    public void UpdateUI()
    {
        Debug.Log("UPDATING EQUIPMENT UI");

        for(int i = 0; i < equipmentSlots.Length; i++)
        {
            if(EquipmentManager.instance.currentEquipments[i] != null && !EquipmentManager.instance.currentEquipments[i].isDefaultItem)
            {
                equipmentSlots[i].SetEquipment(EquipmentManager.instance.currentEquipments[i]);
            }
            else
            {
                equipmentSlots[i].ClearSlot();
            }
        }
    }
}
