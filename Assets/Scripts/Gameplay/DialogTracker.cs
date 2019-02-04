using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DialogTracker : MonoBehaviour
{

    #region singleton

    public static DialogTracker instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("There is more than one dialog tracker!!!");
            return;
        }

        instance = this;
    }

    #endregion

    List<Dialog.Message> messageQueue = new List<Dialog.Message>();

    public void AddMessage(Dialog.Message message, GameObject talker) //adding message to queue and assigning talker as sender of message
    {
        message.SetSender(talker);
        if(message.content != null)
            messageQueue.Add(message);
    }

    public void AddMessage(Dialog.Message message) //adding message to queue if sender and content has been assigned
    {
        if(message.content != null && message.sender != null)
        {
            messageQueue.Add(message);
        }
    }

    public void ShowNext() //showing next messsage of queue in dialog box
    {
        if(messageQueue.Count > 0) //loking always to first element of array allows keeping track of messages and printing one after another like a dialog.
        {
            if (messageQueue[0].dialogType == Dialog.DialogType.Dialog) //if type is dialog than create a dialog box
            {
                CreateHoverUI.instance.CreateADialog(messageQueue[0].content, messageQueue[0].status, null);
                messageQueue.RemoveAt(0);
            }else if(messageQueue[0].dialogType == Dialog.DialogType.Selection) //if type is selectio than create a selection box
            {
                CreateHoverUI.instance.CreateASelection(messageQueue[0].content, messageQueue[0].status, null, messageQueue[0].sender, messageQueue[0].selections);
                messageQueue.RemoveAt(0);
            }
        }
    }
}
