using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignAnimal : MonoBehaviour {
    Adoptante adoptante;
    Canvas canvas;
    GraphicRaycaster graphicRaycaster;
    GraphicsAdoptante graphics;
    int animalId;
    int adoptionPercentage;
    Animal currentAnimal;

    [Header("Info to display (Adopter)")]
    [SerializeField] TextMeshProUGUI adopterSpecie;
    [SerializeField] TextMeshProUGUI adopterSize;
    [SerializeField] TextMeshProUGUI adopterAge;
    [SerializeField] Image adopterSprite;
    [SerializeField] TextMeshProUGUI conversation;

    [Header("Info to display (Animal)")]
    [SerializeField] TextMeshProUGUI animalName;
    [SerializeField] TextMeshProUGUI animalSpecie;
    [SerializeField] TextMeshProUGUI animalSize;
    [SerializeField] TextMeshProUGUI animalAge;
    [SerializeField] GameObject animalDisplayPosition;
    GameObject animalDisplayed;
    public Button[] buttons;
    private void Awake() {
        buttons = GetComponentsInChildren<Button>();
        adoptante = GetComponentInParent<Adoptante>();
        if (adoptante == null) Debug.LogError("Adoptante class not found from " + this.gameObject.name + " gameobject");

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null) Debug.LogError("Canvas not found from " + this.gameObject.name + " gameobject");

        graphics = GetComponentInParent<GraphicsAdoptante>();
        if (graphics == null) Debug.LogError("GraphicsAdoptante class not found from " + this.gameObject.name + " gameobject");

        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    void Start () {
        adopterSpecie.text = adoptante.speciePreferred.ToString();
        adopterAge.text = adoptante.agePreferred.ToString();
        adopterSprite.sprite = graphics.adopterImage;

        switch (adoptante.sizePreferred) {
            case Animal.SIZE.BIG:
                adopterSize.text = "GRANDE";
                break;

            case Animal.SIZE.MEDIUM:
                adopterSize.text = "MEDIANO";
                break;

            case Animal.SIZE.SMALL:
                adopterSize.text = "PEQUEÑO";
                break;
        }

        animalId = 0;

        if (TutorialOverrider.instance != null) {
            buttons[3].onClick.AddListener(() => TutorialOverrider.instance.GoToNextEvent());

            transform.GetChild(6).GetComponentsInChildren<Button>()[0].onClick.AddListener(() => TutorialOverrider.instance.GoToNextEvent());
        }
    }

    private void OnEnable() {
        this.transform.parent = canvas.transform;
        this.transform.localPosition = Vector3.zero;
        UpdateAnimalDisplayedInfo();
    }

    private void OnDisable() {
        //this.transform.parent = graphics.transform;
    }

    void UpdateAnimalDisplayedInfo() {
        if(GameLogic.instance.shelterAnimals.Count == 0) {
            Debug.Log("There is no animal in the shelter");
            return;
        }
        currentAnimal = GameLogic.instance.shelterAnimals[animalId];

        Destroy(animalDisplayed);
        animalDisplayed = Instantiate(GameLogic.instance.animalGraphics[(int)currentAnimal.especie], animalDisplayPosition.transform);
        TintAnimalPart[] t = animalDisplayed.GetComponentsInChildren<TintAnimalPart>();
        foreach(TintAnimalPart i in t) {
            i.ForcePaint(currentAnimal.color);
        }

        animalName.text = currentAnimal.nombre;
        animalSpecie.text = currentAnimal.especie.ToString();
        animalAge.text = currentAnimal.edad.ToString();

        switch (currentAnimal.size) {
            case Animal.SIZE.BIG:
                animalSize.text = "GRANDE";
                break;

            case Animal.SIZE.MEDIUM:
                animalSize.text = "MEDIANO";
                break;

            case Animal.SIZE.SMALL:
                animalSize.text = "PEQUEÑO";
                break;
        }
    }

    #region BUTTONS
    public void Left() {
        animalId--;
        if (animalId < 0) {
            animalId = 0;
            return;
        }
        UpdateAnimalDisplayedInfo();
    }

    public void Right() {
        animalId++;
        if (animalId >= GameLogic.instance.shelterAnimals.Count) {
            animalId = GameLogic.instance.shelterAnimals.Count-1;
            return;
        }
        UpdateAnimalDisplayedInfo();
    }

    public void Assign() {
        //ADOPTION PERCENTAGE
        //Starts with 5%
        //If specie match adds 45%
        //If age match adds 25%
        //If size match adds 20%

        adoptionPercentage = 5;
        if (adoptante.speciePreferred == currentAnimal.especie) adoptionPercentage += 45;
        if (adoptante.agePreferred == currentAnimal.edad) adoptionPercentage += 25;
        if (adoptante.sizePreferred == currentAnimal.size) adoptionPercentage += 20;

        int rng = Random.Range(0, 100);

        if(rng < adoptionPercentage) {
            //ACCEPTED
            print("ANIMAL ACEPTADO");
            conversation.transform.parent.gameObject.SetActive(true);
            conversation.text = HumanCommonInfo.GetAcceptMessage();
            StartCoroutine(AcceptAdoption());
        }
        else {
            //REJECTED
            print("ANIMAL RECHAZADO");
            conversation.transform.parent.gameObject.SetActive(true);
            conversation.text = HumanCommonInfo.GetRejectMessage();
            StartCoroutine(RejectAdoption());
        }
    }
    #endregion

    IEnumerator AcceptAdoption() {
        graphicRaycaster.enabled = false;
        yield return new WaitForSeconds(3.0f);
        graphicRaycaster.enabled = true;
        GameLogic.instance.AddMoney(50);
        GameLogic.instance.reputation += 0.1f;
        GameLogic.instance.adoptedAnimalNames.Add(currentAnimal.name);
        GameLogic.instance.RemoveAnimal(currentAnimal);
        //GameLogic.instance.adoptedAnimalCounter++;
        Destroy(adoptante.gameObject);
        Destroy(this.gameObject);
    }

    IEnumerator RejectAdoption() {
        graphicRaycaster.enabled = false;
        yield return new WaitForSeconds(3.0f);
        graphicRaycaster.enabled = true;
        Destroy(adoptante.gameObject);
        Destroy(this.gameObject);
    }
}
