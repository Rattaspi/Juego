using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
    public static CanvasScript canvasScript;
    public GameObject endWeekButtonObject;
    public GameObject eventHandlerObject;
    public Button acceptButton;
    public Button declineButton;
    public Text eventDescription;
    public Text eventTitle;
    public Event currentDisplayedEvent;
	// Use this for initialization
	void Start () {
        canvasScript = this;
        endWeekButtonObject.SetActive(true);
        eventHandlerObject.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy() {
        canvasScript = null;
    }

    public void DisplayEvent(Event e) {
        if (currentDisplayedEvent != e) {
            eventHandlerObject.SetActive(true);
            acceptButton.onClick.RemoveAllListeners();
            declineButton.onClick.RemoveAllListeners();
            eventTitle.text = e.GetTitle();
            eventDescription.text = e.GetDescription();
            acceptButton.GetComponentInChildren<Text>().text = e.GetAcceptText();
            declineButton.GetComponentInChildren<Text>().text = e.GetDeclineText();
            acceptButton.onClick.AddListener(()=>e.OnAccept());
            declineButton.onClick.AddListener(() => e.OnDeny());
        }
    }

    public void StopEvent() {
        currentDisplayedEvent = null;
        eventHandlerObject.SetActive(false);
    }

}
