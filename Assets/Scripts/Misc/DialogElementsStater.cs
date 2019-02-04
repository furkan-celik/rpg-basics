using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogElementsStater : MonoBehaviour {

    public Text content;
    public Image senderSprite;
    public Image receiverSprite;
    public AudioSource audioSource;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        DialogTracker.instance.ShowNext();
    }
}
