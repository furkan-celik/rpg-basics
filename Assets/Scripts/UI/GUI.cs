using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class GUI : MonoBehaviour {

    public Text itemTextPrefab;
    public Canvas healthBar;

    private Image[] healthBars;

    void Start()
    {
        GameObject[] items = GameObject.FindGameObjectsWithTag("Item");
        foreach(GameObject item in items)
        {
            //Instantiate(itemTextPrefab, new Vector3(item.transform.position.x, item.transform.position.y - 0.5f, item.transform.position.z), Quaternion.identity, item.transform).text = item.GetComponent<ItemPickup>().item.name;
        }

        healthBars = healthBar.GetComponentsInChildren<Image>();
    }

    private void Update()
    {
        for(int i = 0; i < healthBars.Length; i++)
        {
            if(i > Mathf.Round(PlayerManager.instance.player.GetComponent<PlayerStats>().CurrentHealth / 10))
            {
                healthBars[i].enabled = false;
            }
            else
            {
                if(i < Mathf.Round(PlayerManager.instance.player.GetComponent<PlayerStats>().CurrentHealth / 10))
                {
                    healthBars[i].color = new Color(healthBars[i].color.r, healthBars[i].color.g, healthBars[i].color.b, (float)(25.5 / (PlayerManager.instance.player.GetComponent<PlayerStats>().CurrentHealth % 10)));
                }
            }
        }
    }
}
