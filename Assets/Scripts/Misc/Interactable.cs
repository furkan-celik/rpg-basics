using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour {

    public float radius = 3f;
    public Transform interactionTransform;

    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;
    bool isEnemy = false;

    public virtual void Interaction()
    {
        //this method meant to be overwritten by class's child
        Debug.Log("Interaction with " + gameObject.name);
        PlayerManager.instance.interacted = gameObject;
    }

    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, interactionTransform.position);
            if(distance <= radius)
            {
                Interaction();
                hasInteracted = true;
            }else if(isEnemy && EquipmentManager.instance.currentWeapon != null && distance <= EquipmentManager.instance.currentWeapon.range)
            {
                Interaction();
                hasInteracted = true;
            }
        }
        
    }

    public void OnFocused(Transform playerTransform, bool _isEnemy)
    {
        isFocus = true;
        player = playerTransform;
        isEnemy = _isEnemy;
        hasInteracted = false;
    }

    public void OnDeFocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
}