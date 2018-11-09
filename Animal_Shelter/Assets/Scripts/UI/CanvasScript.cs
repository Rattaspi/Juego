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
    public enum CanvasState { IDLE,DISPLAYEVENT,DISPLAYANIMALLIST};
    public CanvasState canvasState;
    public int numAnimals;
	// Use this for initialization
	void Start () {
        canvasScript = this;
        endWeekButtonObject.SetActive(true);
        eventHandlerObject.SetActive(false);
        canvasState = CanvasState.IDLE;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy() {
        canvasScript = null;
    }

    public void DisplayIncomingAnimals() {
        //Aquí hay que hacer que canvas haga un display de una lista de animales que entran con botones
        //Que hagan que dichos animales se acepten/rechacen, tenemos que montar una de esas sliderLists
        //De android que no recuerdo como se llaman

        //if (canvasState == CanvasState.DISPLAYANIMALLIST){

        //} else {
        //    numAnimals = Random.Range(0, 10);



        //}

        //if (numAnimals == 0) {
           GameLogic.instance.gameState = GameLogic.GameState.WEEK;
        //}

    }

    public void DisableAcceptButton(){
        acceptButton.GetComponent<Image>().color = Color.grey;
        acceptButton.onClick.RemoveAllListeners();
        Debug.Log("Disable");
    }

    public void DisableDeclineButton() {
        declineButton.GetComponent<Image>().color = Color.grey;
        acceptButton.onClick.RemoveAllListeners();
    }

    public void ReEnableButtons() {
        declineButton.GetComponent<Image>().color = Color.white;
    }

    public void DisplayEvent(Event e) {
        if (currentDisplayedEvent!=e) {
            canvasState = CanvasState.DISPLAYEVENT;
            ReEnableButtons();
            eventHandlerObject.SetActive(true);
            acceptButton.onClick.RemoveAllListeners();
            declineButton.onClick.RemoveAllListeners();
            eventTitle.text = e.GetTitle();
            eventDescription.text = e.GetDescription();
            acceptButton.GetComponentInChildren<Text>().text = e.GetAcceptText();
            declineButton.GetComponentInChildren<Text>().text = e.GetDeclineText();

            if (e.GetCanBeAccepted()) {
                acceptButton.onClick.AddListener(() => e.OnAccept());
            }

            if (e.GetCanBeDenied()) {
                declineButton.onClick.AddListener(() => e.OnDeny());
            }

            currentDisplayedEvent = e;
        }
    }

    public void StopEvent() {
        currentDisplayedEvent = null;
        eventHandlerObject.SetActive(false);
        canvasState = CanvasState.IDLE;
    }

}
