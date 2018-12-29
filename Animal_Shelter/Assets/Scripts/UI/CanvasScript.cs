using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class CanvasScript : MonoBehaviour {
    public static CanvasScript instance;
    public Transform adopterParent;
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
    public TextMeshProUGUI foodDisplayText;
    public TextMeshProUGUI enteringAnimalsNumber;

    public TextMeshProUGUI foodText;
    public TextMeshProUGUI publicityText;
    public TextMeshProUGUI instalationsText;
    public TextMeshProUGUI cleanUpText;
    public TextMeshProUGUI truckText;
    public GameObject mainGameGroup;

    public GameObject popUpMessageGroup;
    public TextMeshProUGUI popUpText;

    public TextMeshProUGUI timeSpeedText;

    public float noSpaceAlphaValue;
    protected Image[] imageChildren;
    protected TextMeshProUGUI[] textChildren;
    public GameObject reputationMedalsHolder;
    public Image[] medals;
    public  int indexAnimalToDisplay;



    // Use this for initialization
    void Start() {
        timeSpeedText.text = "x" + 1;
        animalElementPrefab = Resources.Load<GameObject>("Prefabs/AnimalElementListPrefab");
        instance = this;
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

        imageChildren = popUpMessageGroup.GetComponentsInChildren<Image>();
        textChildren = popUpMessageGroup.GetComponentsInChildren<TextMeshProUGUI>();
        noSpaceAlphaValue = 0.01f;
        if (reputationMedalsHolder != null) {
            medals = reputationMedalsHolder.GetComponentsInChildren<Image>();
        }
    }

    public void KickAnimals() {
        GameLogic.instance.leftOutAnimalCounter += enteringAnimalList.Count;
        foreach (Animal a in enteringAnimalList) {
            GameLogic.instance.leftOutAnimalNames.Add(a.name);
            GameLogic.instance.reputation -= 0.5f;
            Destroy(a.gameObject);
        }
        enteringAnimalList.Clear();
    }

    void UpdateMedals() {


        for(int i = 0; i < medals.Length; i++) {
            if (GameLogic.instance.reputation >= (i + 1) * 10) {
                medals[i].fillAmount = 1;
            } else {
                medals[i].fillAmount = (GameLogic.instance.reputation - (i * 10)) / 10;
            }
        }

        //if (GameLogic.instance.reputation > 10) {
        //    medals[0].fillAmount = 1;
        //} else {

        //}
        //if (GameLogic.instance.reputation > 20) {
        //    medals[1].fillAmount = 1;

        //} else {
        //    medals[1].fillAmount = (float)((GameLogic.instance.reputation % 10) / 10);
        //}
        //if (GameLogic.instance.reputation > 30) {
        //    medals[2].fillAmount = 1;

        //} else {
        //    medals[2].fillAmount = (float)((GameLogic.instance.reputation % 10) / 10);
        //}
        //if (GameLogic.instance.reputation > 40) {
        //    medals[3].fillAmount = 1;

        //} else {
        //    medals[3].fillAmount = (float)((GameLogic.instance.reputation%10)/10);
        //}

        //if (GameLogic.instance.reputation == 50) {
        //    medals[4].fillAmount = 1;
        //} else {
        //    medals[4].fillAmount = (float)((GameLogic.instance.reputation % 10) / 10);
        //}

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

    public virtual void PopUpNoSpaceMessage(string message) {
        if (!popUpMessageGroup.GetComponent<ScaleReactor>().reacting) {
            noSpaceAlphaValue = 1.0f;
            popUpText.text = message;
            popUpMessageGroup.GetComponent<ScaleReactor>().React();
        }
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

    //1268,425

    public virtual bool AcceptAnimalInShelter(Animal a) {

        if (GameLogic.instance.shelterAnimals.Count < GameLogic.instance.currentAnimalCapacity) {

            enteringAnimalDisplayer.selectedAnimalInList.transform.SetParent(GameLogic.instance.animalObjectParent.transform);

            a.transform.SetParent(GameLogic.instance.animalObjectParent.transform);
            float randomX = Random.Range(300, 1300);
            float randomY = Random.Range(0, 460);

            a.transform.localPosition = new Vector3(randomX, randomY, 0);

            GameLogic.instance.shelterAnimals.Add(enteringAnimalDisplayer.selectedAnimalInList);
            return true;
        }
        PopUpNoSpaceMessage("El refugio esta lleno");
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

    public virtual void AddEnteringAnimal(Animal.SIZE size, Animal.EDAD edad, Animal.ESTADO estado,float salud,float hambre) {
        enteringAnimalButtonObject.SetActive(true);
        Animal animal = Animal.MakeSpecificAnimal(size,edad,estado,salud,hambre).GetComponent<Animal>();

        //canvasScript.enteringAnimalDisplayer.selectedAnimalInList = animal;
        enteringAnimalList.Add(animal);
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
        UpdateMedals();
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

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            debugBool = true;
        }

        if (debugBool) {
            debugBool = false;
            instance.AddEnteringAnimal();
        }
        enteringAnimalsNumber.text = instance.enteringAnimalList.Count.ToString();
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
                foodDisplayText.text = GameLogic.instance.amountOfFood.ToString() + "/" + GameLogic.instance.foodCapacity.ToString();
                break;
            default:
                break;

        }
    }
    


    protected virtual void OnDestroy() {
        instance = null;
    }

    public virtual void DisplayShelterAnimals(bool should) {
        CreateAnimalList();
        instance.canvasState = CanvasState.DISPLAYANIMALLIST;
        animalsWindow.SetActive(should);

        if (!should) {
            instance.canvasState = CanvasState.IDLE;
            //animalsWindow.SetActive(false);
        }

    }

    public virtual void DisplayExpenses(bool should) {
        expensesWindow.SetActive(should);
        if (!should) {
            instance.canvasState = CanvasState.IDLE;
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
            acceptButton.GetComponentInChildren<TextMeshProUGUI>().text = e.GetAcceptText();
            declineButton.GetComponentInChildren<TextMeshProUGUI>().text = e.GetDeclineText();

            if (e.GetCanBeAccepted()) {
                acceptButton.onClick.AddListener(() => e.OnAccept());
                if (TutorialOverrider.instance != null) {
                    acceptButton.onClick.AddListener(() => TutorialOverrider.instance.GoToNextEvent());
                }
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
