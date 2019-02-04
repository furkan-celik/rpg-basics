using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentSlots : MonoBehaviour {

    public Image icon;
    public Image bg;

    Equipment equipment;
    Sprite bgSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetBgSprite(Sprite sprite)
    {
        bgSprite = sprite;
        bg.sprite = bgSprite;
    }

    public void SetEquipment(Equipment newEquipment)
    {
        equipment = newEquipment;

        icon.sprite = newEquipment.icon;
        icon.enabled = true;
    }

    public void ClearSlot()
    {
        equipment = null;

        icon.sprite = null;
        icon.enabled = false;
    }

    public void UnEquipCall()
    {
        EquipmentManager.instance.UnEquip(equipment);
    }
}
