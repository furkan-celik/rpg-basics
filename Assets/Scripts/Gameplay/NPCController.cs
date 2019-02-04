using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : MonoBehaviour {

    public float LookRadius = 10f;

	// Use this for initialization
	void Start () {
        //CreateHoverUI.instance.CreateHeadUI(gameObject.name, CameraManager.instance.GetActiveCamera().WorldToScreenPoint(transform.position));
        if(gameObject.GetComponent<NPC>().inventory != null)
            gameObject.GetComponent<NPC>().inventory.Initialize(); //initialzing inventory of npc
    }
	
	// Update is called once per frame
	void Update () {
		if(Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position) <= LookRadius) //if distance to player is less than lookRadius than face to target
        {
            FaceToTarget();
        }

        if(Vector3.Distance(PlayerManager.instance.player.transform.position, transform.position) > gameObject.GetComponent<NPC>().radius) //if distance to player is less than interaction radius than close tradeUI
        {
            TradeUI.instance.tradeUI.SetActive(false);
        }
	}

    void FaceToTarget()
    {
        Vector3 direction = (PlayerManager.instance.player.transform.position - transform.position).normalized;
        Quaternion lookDirection = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookDirection, Time.deltaTime * 5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
    }
}
