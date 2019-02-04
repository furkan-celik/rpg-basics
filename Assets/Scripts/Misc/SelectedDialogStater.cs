using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedDialogStater : DialogElementsStater {
    
    public List<Text> buttonTexts;
    public List<Button> buttons;
    public Transform buttonParent;
    public Button buttonPrefab;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void CreateSelections(List<Dialog.Selection> selections, GameObject talker)
    {
        for(int i = 0; i < selections.Count; i++)
        {
            if(buttonTexts.Count > i)
            {
                FillButton(selections[i].selectionActions, selections[i].content, talker, i);
            }
            else
            {
                Button newButton = Instantiate<Button>(buttonPrefab, buttonParent);
                Text newText = newButton.gameObject.GetComponentInChildren<Text>();
                buttonTexts.Add(newText);
                buttons.Add(newButton);

                FillButton(selections[i].selectionActions, selections[i].content, talker, i);
            }
        }
    }

    void FillButton(Dialog.SelectionActions selectionActions, string content, GameObject talker, int i)
    {
        buttonTexts[i].text = content;

        if (selectionActions == Dialog.SelectionActions.Close)
        {
            buttons[i].onClick.AddListener(delegate { Close(talker); });
        }
        else if (selectionActions == Dialog.SelectionActions.Trade)
        {
            buttons[i].onClick.AddListener(delegate { Trade(talker); });
            if(PlayerManager.instance.interacted.GetComponent<NPC>().temper > PlayerManager.instance.interacted.GetComponent<NPC>().temperLimit)
            {
                buttons[i].enabled = false;
            }
        }
    }

    void Close(GameObject talked)
    {
        Debug.Log("Player has finished a dialog with " + talked.name);
        Destroy(gameObject);
    }

    void Trade(GameObject interacted)
    {
        Debug.Log("Player started trade with " + interacted.name);
        //Trigger trade system
        TradeUI.instance.TriggerUI(interacted.GetComponent<NPC>().inventory);
        Destroy(gameObject);
    }
}
