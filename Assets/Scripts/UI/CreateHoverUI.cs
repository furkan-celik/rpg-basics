using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateHoverUI : MonoBehaviour {

    #region singleton

    public static CreateHoverUI instance;

    private void Awake()
    {
        instance = this;
    }

    #endregion

    public GameObject equipmentHover;
    public GameObject dialog;
    public GameObject dialogSelection;
    public GameObject headUI;
    public GameObject bargainUI;
    public Text notificationText;
    public Scrollbar cooldownBar;
    public Transform mainCanvas;

    public delegate IEnumerator OnDialogStarted(GameObject gameObject);
    public OnDialogStarted onDialogStartedCallback;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public GameObject[] compareTwoItems(Item item1, Item item2, Transform parent)
    {
        GameObject[] result = new GameObject[2];
        result[0] = CreateEquipmentHoverUI(item1.name, item1.icon, item1.GetPower(), item1.itemClass, item1.rareType, item1.sellPrice, item1.buyPrice, item1.EquipState, parent);
        result[1] = CreateEquipmentHoverUI(item2.name, item2.icon, item2.GetPower(), item2.itemClass, item2.rareType, item2.sellPrice, item2.buyPrice, item2.EquipState, parent);
        
        result[1].GetComponent<RectTransform>().anchoredPosition = new Vector2(-350, 0);

        return result;
    }

    public GameObject CreateEquipmentHoverUI(string name, Sprite icon, int power, PlayerStats.PlayerClasses itemClass, Item.RareTypes rareType, int sellPrice, int buyPrice, bool isEquipped, Transform parent)
    {
        GameObject currentHover = Instantiate(equipmentHover, parent);
        HoverElementsStater hes = currentHover.GetComponent<HoverElementsStater>();
        Text itemName = hes.itemName;

        if (isEquipped)
            itemName.text = name + " (Equipped)";
        else
            itemName.text = name;
        hes.power.text = power.ToString();
        hes.itemClass.text = itemClass.ToString();
        hes.sellPrice.text = sellPrice.ToString();
        hes.buyPrice.text = buyPrice.ToString();
        hes.icon.sprite = icon;


        if(PlayerManager.instance.player.GetComponent<PlayerStats>().playerClass != itemClass)
        {
            hes.itemClass.color = Color.red;
        }
        else
        {
            hes.itemClass.color = Color.green;
        }

        switch ((int)rareType)
        {
            case 0:
                itemName.color = Color.black;
                break;
            case 1:
                itemName.color = Color.cyan;
                break;
            case 2:
                itemName.color = Color.magenta;
                break;
            case 3:
                itemName.color = Color.yellow;
                break;
            default:
                itemName.color = Color.gray;
                break;
        }
        
        if(parent.position.x < 500)
        {
            currentHover.GetComponent<RectTransform>().anchoredPosition = new Vector2(160, currentHover.GetComponent<RectTransform>().anchoredPosition.y);
        }

        currentHover.transform.SetAsLastSibling();

        Debug.Log("An hover menu created. Item name: " + itemName.text);

        return currentHover;
    }

    public GameObject CreateADialog(string content, MessageStatus messageStatus, Sprite talker)
    {
        GameObject result = Instantiate(dialog, mainCanvas);

        DialogElementsStater des = result.GetComponent<DialogElementsStater>();
        des.content.text = content;
        
        switch (messageStatus)
        {
            case MessageStatus.Sender:

                des.receiverSprite.enabled = false;
                des.senderSprite.enabled = true;
                if(talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
            case MessageStatus.Reciever:

                des.receiverSprite.enabled = true;
                des.senderSprite.enabled = false;
                if(talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
        }

        Debug.Log("A dialog box created. Message is: " + content);
        onDialogStartedCallback.Invoke(result);

        return result;
    }

    public GameObject CreateADialog(string content, MessageStatus messageStatus, Sprite talker, AudioClip sound)
    {
        GameObject result = Instantiate(dialog);

        DialogElementsStater des = result.GetComponent<DialogElementsStater>();
        des.content.text = content;

        switch (messageStatus)
        {
            case MessageStatus.Sender:

                des.receiverSprite.enabled = false;
                des.senderSprite.enabled = true;
                if (talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
            case MessageStatus.Reciever:

                des.receiverSprite.enabled = true;
                des.senderSprite.enabled = false;
                if (talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
        }

        if(sound != null)
            des.audioSource.clip = sound;

        Debug.Log("A dialog box with sound created. Message is: " + content + " Sound name is: " + sound.name + " at load state " + sound.loadState);
        onDialogStartedCallback.Invoke(result);

        return result;
    }

    public GameObject CreateASelection(string content, MessageStatus messageStatus, Sprite talker, GameObject talkerObject, List<Dialog.Selection> selections)
    {
        GameObject result = Instantiate(dialogSelection, mainCanvas);

        SelectedDialogStater des = result.GetComponent<SelectedDialogStater>();
        des.content.text = content;

        switch (messageStatus)
        {
            case MessageStatus.Sender:

                des.receiverSprite.enabled = false;
                des.senderSprite.enabled = true;
                if (talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
            case MessageStatus.Reciever:

                des.receiverSprite.enabled = true;
                des.senderSprite.enabled = false;
                if (talker != null)
                {
                    des.receiverSprite.sprite = talker;
                }

                break;
        }

        des.CreateSelections(selections, talkerObject);

        Debug.Log("A dialog box created. Message is: " + content);
        onDialogStartedCallback.Invoke(result);

        return result;
    }

    public void CreateHeadUI(string content, Vector3 position)
    {
        GameObject result = Instantiate(headUI, position, Quaternion.identity, mainCanvas);

        HeadUIStater hus = result.GetComponent<HeadUIStater>();
        hus.content.text = content;
    }

    public void CreateBargain(string itemName, Sprite icon, int basePrice)
    {
        GameObject result = Instantiate(bargainUI, mainCanvas);

        BargainElementsStater bes = result.GetComponent<BargainElementsStater>();

        bes.itemName.text = itemName;
        bes.icon.sprite = icon;
        bes.inputField.text = basePrice.ToString();
    }

    public void CreateBargain(Item item, int basePrice)
    {
        GameObject result = Instantiate(bargainUI, mainCanvas);

        BargainElementsStater bes = result.GetComponent<BargainElementsStater>();

        bes.itemName.text = item.name;
        bes.icon.sprite = item.icon;
        bes.inputField.text = basePrice.ToString();
        bes.item = item;
        bes.npcFinanceType = PlayerManager.instance.interacted.GetComponent<NPC>().finance;
    }

    public void CreateNotification(string content)
    {
        Text result = Instantiate<Text>(notificationText, mainCanvas);
        result.text = content;
        Debug.Log("Notification has been created. Content is " + content);
        Invoke("DestroyNotification", 1f);
    }

    private void DestroyNotification()
    {
        int counter = 0;
        foreach(var item in GameObject.FindGameObjectsWithTag("Notification"))
        {
            Destroy(item);
            counter++;
        }
        Debug.Log(counter + " notification has been destroyed.");
    }

    public void CreateCooldownUI(float time)
    {
        Scrollbar scrollbar = Instantiate<Scrollbar>(cooldownBar, mainCanvas);
        CooldownBarStater cbs = scrollbar.GetComponent<CooldownBarStater>();
        cbs.Initialize(time);
    }

    public enum MessageStatus { Sender, Reciever }
}
