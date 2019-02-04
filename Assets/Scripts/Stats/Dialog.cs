using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "New Dialog", menuName = "Dialog/New Dialog")]
public class Dialog : ScriptableObject {

    [System.Serializable]
    public struct Message
    {
        public string content;
        public CreateHoverUI.MessageStatus status;
        public DialogType dialogType;

        public List<Selection> selections;

        public GameObject sender;
        public float timeStamp;

        public void SetSender(GameObject input)
        {
            sender = input;
        }

        public void SetTimeStamp(float input)
        {
            timeStamp = input;
        }
    }

    [System.Serializable]
    public struct Selection
    {
        public string content;
        public SelectionActions selectionActions;
    }

    public List<Message> dialog;

    public enum DialogType { Dialog, Selection }
    public enum SelectionActions { Close, Trade }
}
