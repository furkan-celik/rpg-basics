using System;
using UnityEngine;
using UnityEngine.UI;

public class BargainElementsStater : MonoBehaviour
{

    public Text itemName;
    public Image icon;
    public InputField inputField;
    public Button inputButton;
    public Item item;
    public NPC.Finance npcFinanceType;
    public InteractionType interactionType;

    private int startPrice;
    private int offer;
    private int attemptCount = 0;
    private int agresiveness = 0;

    private const string NotIntegerError =      "Input is not a integer.";
    private const string NegativeInputError =   "Input is negative";
    private const string LowMoneyError =        "Input is less than player's money";

    private const string AgroBadRespond =  "Go away and fun with other people. I won't take this behaviour.";
    private const string AgroNeuRespond =  "Go the fuck away.";
    private const string AgroGoodRespond = "That could be a good business.";

    private const string NeuBadRespond =  "I don't want to make a trade with you poor.";
    private const string NeuNeuRespond =  "Whether is not suitable for trade.";
    private const string NeuGoodRespond = "Not today, I think...";

    private const string FriBadRespond =  "Maybe another time.";
    private const string FriNeuRespond =  "At least, I'm happy to see you.";
    private const string FriGoodRespond = "I would like to see you again.";

    private void Start()
    {
        startPrice = int.Parse(inputField.text);
        if (startPrice == item.buyPrice)
        {
            interactionType = InteractionType.Buy;
        }
        else if (startPrice == item.sellPrice)
        {
            interactionType = InteractionType.Sell;
        }
        agresiveness = PlayerManager.instance.interacted.GetComponent<NPC>().temper;
    }

    public void GiveOffer()
    {
        try
        {
            offer = int.Parse(inputField.text);
            int difference = offer - startPrice;
            int maxAcceptableDifference = item.sellPrice - item.buyPrice;
            attemptCount++;

            if (attemptCount > 3)
            {
                Reject();
                return;
            }
            GetAngry();

            if (PlayerManager.instance.player.GetComponent<PlayerStats>().money >= offer)
            {
                if (agresiveness > PlayerManager.instance.interacted.GetComponent<NPC>().temperLimit)
                    TradeUI.instance.tradeUI.SetActive(false);

                switch (npcFinanceType)
                {
                    case NPC.Finance.LowEsteem:
                        if ((difference > (maxAcceptableDifference - 1) || difference >= 0
                            || ((interactionType == InteractionType.Sell && offer < item.sellPrice)
                            || (interactionType == InteractionType.Buy && offer > item.buyPrice)))
                            && ((interactionType == InteractionType.Sell && offer < item.buyPrice)
                            || (interactionType == InteractionType.Buy && offer > item.sellPrice)))
                        {
                            Accept();
                        }
                        else
                        {
                            CounterOffer();
                        }
                        break;
                    case NPC.Finance.Average:
                        if ((difference > (maxAcceptableDifference + 1) || difference >= 0
                            || ((interactionType == InteractionType.Sell && offer < item.sellPrice)
                            || (interactionType == InteractionType.Buy && offer > item.buyPrice)))
                            && ((interactionType == InteractionType.Sell && offer < item.buyPrice)
                            || (interactionType == InteractionType.Buy && offer > item.sellPrice)))
                        {
                            Accept();
                        }
                        else
                        {
                            CounterOffer();
                        }
                        break;
                    case NPC.Finance.HighEsteem:
                        if ((difference > (maxAcceptableDifference + 2) || difference >= 0 
                            || ((interactionType == InteractionType.Sell && offer < item.sellPrice)
                            || (interactionType == InteractionType.Buy && offer > item.buyPrice)))
                            && ((interactionType == InteractionType.Sell && offer < item.buyPrice)
                            || (interactionType == InteractionType.Buy && offer > item.sellPrice)))
                        {
                            Accept();
                        }
                        else
                        {
                            CounterOffer();
                        }
                        break;
                }
            }
            else
            {
                Debug.Log("Not enough funds. Offer: " + offer + " balance: " + PlayerManager.instance.player.GetComponent<PlayerStats>().money);
            }
        }
        catch (Exception ex)
        {
            Debug.LogException(ex);
        }
    }

    void Accept()
    {
        switch (interactionType)
        {
            case InteractionType.Buy:
                Inventory.instance.AddItem(item);
                TradeUI.instance.inventory.DeleteItem(item);
                PlayerManager.instance.player.GetComponent<PlayerStats>().money -= offer;
                Debug.Log("Player has bought " + item.name + " from " + TradeUI.instance.inventory.name);
                TradeUI.instance.UpdateUI();
                break;
            case InteractionType.Sell:
                PlayerManager.instance.player.GetComponent<PlayerStats>().money += offer;
                Debug.Log("Player has sold " + item.name + " to " + TradeUI.instance.inventory.name + " for " + item.sellPrice + " gold.");
                TradeUI.instance.inventory.AddItem(item);
                Inventory.instance.DeleteItem(item);
                TradeUI.instance.UpdateUI();
                break;
        }
        Destroy(gameObject);
    }

    void CounterOffer()
    {
        attemptCount++;

        if (attemptCount > 3)
        {
            Reject();
            return;
        }

        switch (npcFinanceType)
        {
            case NPC.Finance.LowEsteem:
                switch (interactionType)
                {
                    case InteractionType.Sell:
                        Mathf.Clamp(++startPrice, item.sellPrice, item.buyPrice);
                        NewOffer(startPrice, null);
                        break;
                    case InteractionType.Buy:
                        Mathf.Clamp(--startPrice, item.sellPrice, item.buyPrice);
                        NewOffer(startPrice, null);
                        break;
                }
                break;
            case NPC.Finance.Average:
                NewOffer(startPrice, null);
                break;
            case NPC.Finance.HighEsteem:
                switch (interactionType)
                {
                    case InteractionType.Sell:
                        NewOffer(--startPrice, null);
                        break;
                    case InteractionType.Buy:
                        NewOffer(++startPrice, null);
                        break;
                }
                break;
        }
    }

    void GetAngry()
    {
        if((interactionType == InteractionType.Buy && offer < item.sellPrice) || (interactionType == InteractionType.Sell && offer > item.buyPrice))
        {
            agresiveness += 10;
        }
        else if ((interactionType == InteractionType.Buy && offer > item.sellPrice) || (interactionType == InteractionType.Sell && offer < item.buyPrice))
        {
            agresiveness -= 10;
        }
    }

    void Reject()
    {
        Dialog.Message rejectMessage = CreateRejectMessage();
        DialogTracker.instance.AddMessage(rejectMessage, rejectMessage.sender);
        DialogTracker.instance.ShowNext();
        if(rejectMessage.sender.GetComponent<NPC>().civilType == NPC.CivilType.Agressive)
        {
            TradeUI.instance.inventory.CloseItemToSale(item);
            TradeUI.instance.tradeUI.SetActive(false);
            Destroy(gameObject);
        }
        else if(rejectMessage.sender.GetComponent<NPC>().civilType == NPC.CivilType.Neutural)
        {
            TradeUI.instance.inventory.CloseItemToSale(item);
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    Dialog.Message CreateRejectMessage()
    {
        string respond = "";

        switch (PlayerManager.instance.interacted.GetComponent<NPC>().civilType)
        {
            case NPC.CivilType.Agressive:
                if (agresiveness > 50)
                    respond = AgroBadRespond;
                else if (agresiveness > 35 && agresiveness <= 50)
                    respond = AgroNeuRespond;
                else
                    respond = AgroGoodRespond;
                break;
            case NPC.CivilType.Neutural:
                if (agresiveness > 65)
                    respond = NeuBadRespond;
                else if (agresiveness > 45 && agresiveness <= 65)
                    respond = NeuNeuRespond;
                else
                    respond = NeuGoodRespond;
                break;
            case NPC.CivilType.Friendly:
                if (agresiveness > 80)
                    respond = FriBadRespond;
                else if (agresiveness > 50 && agresiveness <= 80)
                    respond = FriNeuRespond;
                else
                    respond = FriGoodRespond;
                break;
        }

        Dialog.Message result = new Dialog.Message
        {
            content = respond,
            dialogType = Dialog.DialogType.Dialog,
            sender = PlayerManager.instance.interacted,
            timeStamp = Time.time
        };
        return result;
    }

    void NewOffer(int amount, AudioClip sound)
    {
        inputField.text = amount.ToString();

        if(sound != null)
        {
            //Play sound
        }
    }

    public void InputChecker()
    {
        int input;
        if (!int.TryParse(inputField.text, out input))
        {
            Debug.Log(NotIntegerError);
            CreateHoverUI.instance.CreateNotification(NotIntegerError);
            inputButton.enabled = false;
        }
        else if (input < 0)
        {
            Debug.Log(NegativeInputError);
            CreateHoverUI.instance.CreateNotification(NegativeInputError);
            inputButton.enabled = false;
        }
        else if (PlayerManager.instance.player.GetComponent<PlayerStats>().money < input)
        {
            Debug.Log(LowMoneyError + "Offer: " + input + " balance: " + PlayerManager.instance.player.GetComponent<PlayerStats>().money);
            CreateHoverUI.instance.CreateNotification(LowMoneyError);
            inputButton.enabled = false;
        }
        else
        {
            inputButton.enabled = true;
        }
    }

    private void OnDestroy()
    {
        PlayerManager.instance.interacted.GetComponent<NPC>().temper += agresiveness;
    }

    public enum InteractionType { Buy, Sell }
}
