using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour {
    public static CanvasScript canvasScript;
    public SelectedAnimalDisplayer selectedAnimalDisplayer;
    public GameObject UIManagementGameObject;
    public GameObject eventHandlerObject;
    public Button acceptButton;
    public Button declineButton;
    public TextMeshProUGUI eventDescription;
    public TextMeshProUGUI eventTitle;
    public Event currentDisplayedEvent;
    public enum CanvasState { IDLE, DISPLAYEVENT, DISPLAYANIMALLIST, DISPLAYEXPENSES };
    public CanvasState canvasState;
    //public GameObject incomingAnimalListObject;
    public GameObject gridObject;
    public GameObject amountOfAnimalsFeedBack;
    public TextMeshProUGUI amountOfAnimalsFeedBackText;
    public GameObject tempAnimalParent;
    public GameObject expensesWindow;
    public GameObject animalsWindow;

    protected GameObject animalElementPrefab;

    public List<Animal> enteringAnimalList;

    public Animal animalOnListClickedOnto;

    public int numAnimals;
    public List<AnimalElementList> animalElements;

    public EnteringAnimalDisplayer enteringAnimalDisplayer;
    //public Animal enteringAnimal;
    public GameObject enteringAnimalButtonObject;
    public bool debugBool;
    public TextMeshProUGUI totalExpensesText;
    public TextMeshProUGUI moneyText;

    public TextMeshProUGUI enteringAnimalsNumber;

    public TextMeshProUGUI foodText;
    public TextMeshProUGUI publicityText;
    public TextMeshProUGUI instalationsText;
    public TextMeshProUGUI cleanUpText;
    public TextMeshProUGUI truckText;
    public GameObject mainGameGroup;

    public GameObject noSpaceMessageGroup;
    public float noSpaceAlphaValue;
    protected Image[] imageChildren;
    protected TextMeshProUGUI[] textChildren;


    public  int indexAnimalToDisplay;
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
        tempAnimalParent.transform.position = new Vector3(-1300, 0, 0);
        tempAnimalParent.transform.localPosition = new Vector3(-1300, 0, 0);

        tempAnimalParent.name = "tempAnimalParent";
        tempAnimalParent.transform.parent = gameObject.transform;
        canvasState = CanvasState.IDLE;
        enteringAnimalButtonObject.SetActive(false);
        enteringAnimalList = new List<Animal>();

        imageChildren = noSpaceMessageGroup.GetComponentsInChildren<Image>();
        textChildren = noSpaceMessageGroup.GetComponentsInChildren<TextMeshProUGUI>();
        noSpaceAlphaValue = 0.01f;
    }

    public virtual void SetDisplayIndex(int index) {
        //Debug.Log("TrynaSet");

        if (index >= 0) {
            if (index < enteringAnimalList.Count) {
                //Debug.Log("Set1");

                indexAnimalToDisplay = index;
                enteringAnimalDisplayer.selectedAnimalInList = enteringAnimalList[indexAnimalToDisplay];
                enteringAnimalDisplayer.SetInfo(enteringAnimalDisplayer.selectedAnimalInList);
            } else {
                //Debug.Log("Set2");

                indexAnimalToDisplay = index;
                enteringAnimalDisplayer.selectedAnimalInList = enteringAnimalList[enteringAnimalList.Count - 1];
                enteringAnimalDisplayer.SetInfo(enteringAnimalDisplayer.selectedAnimalInList);
            }
        } else {
            //Debug.Log("Set3");
            indexAnimalToDisplay = index;
            enteringAnimalDisplayer.selectedAnimalInList = enteringAnimalList[0];
            enteringAnimalDisplayer.SetInfo(enteringAnimalDisplayer.selectedAnimalInList);
        }
    }

    public virtual void IncreaseDisplayIndex(int index) {
        int tempIndex = indexAnimalToDisplay + index;
        if (tempIndex >= enteringAnimalList.Count) {
            tempIndex = 0;
        } else if (tempIndex < 0) {
            tempIndex = enteringAnimalList.Count - 1;
        }
        indexAnimalToDisplay = tempIndex;
        if (enteringAnimalDisplayer.gameObject.activeInHierarchy) {
            enteringAnimalDisplayer.selectedAnimalInList = enteringAnimalList[indexAnimalToDisplay];
            enteringAnimalDisplayer.SetInfo(enteringAnimalDisplayer.selectedAnimalInList);
        }
    }

    public virtual void PopUpNoSpaceMessage() {
        noSpaceAlphaValue = 1.0f;
        noSpaceMessageGroup.GetComponent<ScaleReactor>().React();
    }

    public virtual void ResolveAnimalRequest(bool accepted) {

        if (accepted) {

            if (AcceptAnimalInShelter(enteringAnimalDisplayer.selectedAnimalInList)) {

                enteringAnimalList.Remove(enteringAnimalDisplayer.selectedAnimalInList);

                if (enteringAnimalList.Count > 0) {
                    SetDisplayIndex(indexAnimalToDisplay - 1);
                }

                //enteringAnimalDisplayer.selectedAnimalInList = null;
            } 
            //Destroy(enteringAnimalDisplayer.currentAnimalPreview);
        } else {
            enteringAnimalList.Remove(enteringAnimalDisplayer.selectedAnimalInList);



            if (enteringAnimalDisplayer.selectedAnimalInList != null) {
                if (enteringAnimalDisplayer.selectedAnimalInList.gameObject != null) {
                    Destroy(enteringAnimalDisplayer.selectedAnimalInList.gameObject);
                }
            }

            if (enteringAnimalList.Count > 0)
                SetDisplayIndex(indexAnimalToDisplay - 1);

            //Destroy(enteringAnimalDisplayer.currentAnimalPreview);
        }
        if (enteringAnimalList.Count == 0) {
            enteringAnimalDisplayer.backgroundObject.SetActive(false);
            enteringAnimalButtonObject.SetActive(false);
        }
    }

    public virtual bool AcceptAnimalInShelter(Animal a) {

        if (GameLogic.instance.shelterAnimals.Count < GameLogic.instance.currentAnimalCapacity) {

            enteringAnimalDisplayer.selectedAnimalInList.transform.SetParent(GameLogic.instance.animalObjectParent.transform);

            a.transform.SetParent(GameLogic.instance.animalObjectParent.transform);
            float randomX = Random.Range(300, 1300);
            float randomY = Random.Range(100, 900);

            a.transform.localPosition = new Vector3(randomX, randomY, 0);

            GameLogic.instance.shelterAnimals.Add(enteringAnimalDisplayer.selectedAnimalInList);
            return true;
        }
        PopUpNoSpaceMessage();
        return false;
    }

    public virtual void AddEnteringAnimal() {
        enteringAnimalButtonObject.SetActive(true);
        Animal animal = Animal.MakeARandomAnimal().GetComponent<Animal>();

        //canvasScript.enteringAnimalDisplayer.selectedAnimalInList = animal;
        enteringAnimalList.Add(animal);

        //Debug.Log(canvasScript.enteringAnimalDisplayer.selectedAnimalInList);
        //Aqui hay que meter el código para que se añada un animal
    }

    public virtual void DisplayEnteringAnimal() {
        //enteringAnimalObject.SetActive(true);
    }

    public virtual void SelectAnimal(AnimalElementList animal) {
        //if (selectedAnimalDisplayer != null) {
        //    selectedAnimalDisplayer.selectedAnimalInList = animal.associatedAnimal;
        //}
        Debug.Log(animal.associatedAnimal);
        animalOnListClickedOnto = animal.associatedAnimal;
        selectedAnimalDisplayer.SetInfo(animalOnListClickedOnto);

    }

    // Update is called once per frame
    void Update() {

        amountOfAnimalsFeedBackText.text = GameLogic.instance.shelterAnimals.Count.ToString() + "/" + GameLogic.instance.currentAnimalCapacity;

        if (noSpaceAlphaValue > 0) {
            noSpaceAlphaValue -= Time.deltaTime;

            if (noSpaceAlphaValue < 0) {
                noSpaceAlphaValue = 0;
            }

            for(int i = 0; i < imageChildren.Length; i++) {
                imageChildren[i].color = new Color(imageChildren[i].color.r, imageChildren[i].color.g, imageChildren[i].color.b, noSpaceAlphaValue);
            }

            for(int i = 0; i < textChildren.Length; i++) {
                textChildren[i].color = new Color(textChildren[i].color.r, textChildren[i].color.g, textChildren[i].color.b, noSpaceAlphaValue);

            }

        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            debugBool = true;
        }

        if (debugBool) {
            debugBool = false;
            canvasScript.AddEnteringAnimal();
        }
        enteringAnimalsNumber.text = canvasScript.enteringAnimalList.Count.ToString();
        switch (canvasState) {
            case CanvasState.DISPLAYANIMALLIST:
                if (selectedAnimalDisplayer != null) {
                    if (selectedAnimalDisplayer.selectedAnimalInList != animalOnListClickedOnto) {
                        selectedAnimalDisplayer.SetInfo(animalOnListClickedOnto);
                    }
                }
                break;
            case CanvasState.IDLE:
                totalExpensesText.text = "Total : " + CommonMethods.GetNumberWithDots((int)GameLogic.instance.GetToggleOptionsMoney()) + "€";
                foodText.text = "Compra de comida (" + GameLogic.instance.currentFoodExpense.ToString() + "€)";
                publicityText.text = "Gastos en publicidad (" + GameLogic.instance.currentPublicityExpense.ToString() + "€)";
                instalationsText.text = "Gastos local (" + GameLogic.instance.currentInstalationsExpense.ToString() + "€)";
                cleanUpText.text = "Limpieza (" + GameLogic.instance.currentCleanUpExpense.ToString() + "€)";
                truckText.text = "Gastos en furgoneta (busca animales) (" + GameLogic.instance.currentGasExpense.ToString() + "€)";

                moneyText.text = CommonMethods.GetNumberWithDots((int)GameLogic.instance.money) + "€";
                break;
            default:
                break;

        }
    }
    


    protected virtual void OnDestroy() {
        canvasScript = null;
    }

    public virtual void DisplayShelterAnimals(bool should) {
        CreateAnimalList();
        canvasScript.canvasState = CanvasState.DISPLAYANIMALLIST;
        animalsWindow.SetActive(should);

        if (!should) {
            canvasScript.canvasState = CanvasState.IDLE;
            //animalsWindow.SetActive(false);
        }

    }

    public virtual void DisplayExpenses(bool should) {
        expensesWindow.SetActive(should);
        if (!should) {
            canvasScript.canvasState = CanvasState.IDLE;
        }
    }


    public virtual void CreateAnimalList() {
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

    public virtual bool IsNull(AnimalElementList g) {
        return g == null;
    }

    public virtual void DisableAcceptButton() {
        acceptButton.GetComponent<Image>().color = Color.grey;
        acceptButton.onClick.RemoveAllListeners();
        Debug.Log("Disable");
    }

    public virtual void DisableDeclineButton() {
        declineButton.GetComponent<Image>().color = Color.grey;
        acceptButton.onClick.RemoveAllListeners();
    }

    public virtual void ReEnableButtons() {
        declineButton.GetComponent<Image>().color = Color.white;
    }

    public virtual void DisplayEvent(Event e) {
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

    public virtual void StopEvent() {
        currentDisplayedEvent = null;
        eventHandlerObject.SetActive(false);
        canvasState = CanvasState.IDLE;
        UIManagementGameObject.SetActive(true);
    }

}
