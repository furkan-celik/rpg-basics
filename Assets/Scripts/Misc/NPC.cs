using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Interactable {

    public Dialog dialog;
    public InventorySO inventory;
    public Finance finance;
    public CivilType civilType;
    public int temper = 0;
    public int temperLimit = 100;
    public bool openToMarket = true;

    public override void Interaction()
    {
        base.Interaction();

        if (PlayerManager.instance.player.gameObject.GetComponent<PlayerStats>().onAttack)
        {
            Debug.Log("Player is on attack state, can't talk.");
            return;
        }
        
        foreach(Dialog.Message item in dialog.dialog)
        {
            if(item.content != null)
                DialogTracker.instance.AddMessage(item, gameObject);
        }
        DialogTracker.instance.ShowNext();
    }

    public enum Finance { LowEsteem, Average, HighEsteem }
    public enum CivilType { Agressive, Neutural, Friendly }
}
