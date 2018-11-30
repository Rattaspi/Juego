using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasScript : MonoBehaviour {
    public static CanvasScript canvasScript;
    public SelectedAnimalDisplayer selectedAnimalDisplayer;
    public GameObject UIManagementGameObject;
    public GameObject eventHandlerObject;
    public Button acceptButton;
    public Button declineButton;
    public Text eventDescription;
    public Text eventTitle;
    public Event currentDisplayedEvent;
    public enum CanvasState { IDLE, DISPLAYEVENT, DISPLAYANIMALLIST };
    public CanvasState canvasState;
    //public GameObject incomingAnimalListObject;
    public GameObject gridObject;
    public GameObject amountOfAnimalsFeedBack;
    public Text amountOfAnimalsFeedBackText;
    public GameObject tempAnimalParent;
    public GameObject expensesWindow;
    public GameObject animalsWindow;

    GameObject animalElementPrefab;

    public Animal animalOnListClickedOnto;

    public int numAnimals;
    public List<AnimalElementList> animalElements;
    // Use this for initialization
    void Start() {
        animalElementPrefab = Resources.Load<GameObject>("Prefabs/AnimalElementListPrefab");
        canvasScript = this;
        UIManagementGameObject.SetActive(true);
        eventHandlerObject.SetActive(false);
        //incomingAnimalListObject.SetActive(false);
        expensesWindow.SetActive(false);
        animalsWindow.SetActive(false);
        tempAnimalParent = new GameObject();
        tempAnimalParent.name = "tempAnimalParent";
        tempAnimalParent.transform.parent = gameObject.transform;
        canvasState = CanvasState.IDLE;
    }

    public void SelectAnimal(AnimalElementList animal) {
        //if (selectedAnimalDisplayer != null) {
        //    selectedAnimalDisplayer.selectedAnimalInList = animal.associatedAnimal;
        //}
        animalOnListClickedOnto = animal.associatedAnimal;
    }

    // Update is called once per frame
    void Update() {
        switch (canvasState) {
            case CanvasState.DISPLAYANIMALLIST:
                if (selectedAnimalDisplayer != null) {
                    if (selectedAnimalDisplayer.selectedAnimalInList != animalOnListClickedOnto) {
                        selectedAnimalDisplayer.SetInfo(animalOnListClickedOnto);
                    }
                }
                break;
            default:
                break;

        }
    }

    private void OnDestroy() {
        canvasScript = null;
    }

    public void StopDisplayingShelterAnimals() {
        canvasScript.canvasState = CanvasState.IDLE;
        animalsWindow.SetActive(false);
    }

    public void DisplayShelterAnimals() {
        CreateAnimalList();
        canvasScript.canvasState = CanvasState.DISPLAYANIMALLIST;
        animalsWindow.SetActive(true);
    }

    public void DisplayExpenses() {
        expensesWindow.SetActive(true);
    }

    public void CreateAnimalList() {
        if (animalElements.Count > 0) {
            foreach (AnimalElementList a in animalElements) {
                Destroy(a.gameObject);
            }
            animalElements.RemoveAll(IsNull);
        }
        animalElements.Clear();

        for (int i = 0; i < GameLogic.instance.shelterAnimals.Count; i++) {
            GameObject anAnimalElement = Instantiate<GameObject>(animalElementPrefab);
            anAnimalElement.transform.SetParent(gridObject.transform);
            animalElements.Add(anAnimalElement.GetComponent<AnimalElementList>());
            anAnimalElement.GetComponent<AnimalElementList>().AssociateAnimal(GameLogic.instance.shelterAnimals[i]);
        }

    }

    //public void DisplayAllAnimals() {
    //    if (canvasState == CanvasState.DISPLAYANIMALLIST) {
    //        amountOfAnimalsFeedBackText.text = "Espacio: " + GameLogic.instance.shelterAnimals.Count + "/" + GameLogic.instance.currentAnimalCapacity;

    //    }
    //}

    //    public void DisplayIncomingAnimals() {
    //    //Aquí hay que hacer que canvas haga un display de una lista de animales que entran con botones
    //    //Que hagan que dichos animales se acepten/rechacen, tenemos que montar una de esas sliderLists
    //    //De android que no recuerdo como se llaman
    //    incomingAnimalListObject.SetActive(true);
    //    amountOfAnimalsFeedBack.SetActive(true);

    //    if (canvasState == CanvasState.DISPLAYANIMALLIST) {
    //        amountOfAnimalsFeedBackText.text = "Espacio: " + GameLogic.instance.shelterAnimals.Count + "/" + GameLogic.instance.currentAnimalCapacity;
    //    } else {
    //        numAnimals = Random.Range(0, 10);

    //        for (int i = 0; i < numAnimals; i++) {
    //            GameObject anAnimalElement = Instantiate<GameObject>(animalElementPrefab);
    //            anAnimalElement.transform.SetParent(gridObject.transform);
    //            animalElements.Add(anAnimalElement.GetComponent<AnimalElementList>());

    //            GameObject animalObject = new GameObject();
    //            Animal animal = animalObject.AddComponent<Animal>();
    //            animalObject.transform.SetParent(tempAnimalParent.transform);
    //            animal.StartStats();
    //            animalObject.name = animal.nombre;
    //            anAnimalElement.GetComponent<AnimalElementList>().AssociateAnimal(animal);
    //        }
    //        canvasState = CanvasState.DISPLAYANIMALLIST;
    //    }

    //    animalElements.RemoveAll(IsNull);

    //    if (animalElements.Count==0) {
    //        GameLogic.instance.gameState = GameLogic.GameState.WEEK;
    //        UIManagementGameObject.SetActive(true);
    //        incomingAnimalListObject.SetActive(false);
    //        amountOfAnimalsFeedBack.SetActive(false);
    //    }

    //}

    bool IsNull(AnimalElementList g) {
        return g == null;
    }

    public void DisableAcceptButton() {
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
        UIManagementGameObject.SetActive(false);
        if (currentDisplayedEvent != e) {
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
        UIManagementGameObject.SetActive(true);
    }

}
