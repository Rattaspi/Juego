using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AssignAnimal : MonoBehaviour {
    Adoptante adoptante;
    Canvas canvas;
    GraphicsAdoptante graphics;
    int animalId;

    [Header("Info to display (Adopter)")]
    [SerializeField] TextMeshProUGUI adopterSpecie;
    [SerializeField] TextMeshProUGUI adopterSize;
    [SerializeField] TextMeshProUGUI adopterAge;
    [SerializeField] Image adopterSprite;

    [Header("Info to display (Animal)")]
    [SerializeField] TextMeshProUGUI animalName;
    [SerializeField] TextMeshProUGUI animalSpecie;
    [SerializeField] TextMeshProUGUI animalSize;
    [SerializeField] TextMeshProUGUI animalAge;
    [SerializeField] GameObject animalDisplayPosition;

    private void Awake() {
        adoptante = GetComponentInParent<Adoptante>();
        if (adoptante == null) Debug.LogError("Adoptante class not found from " + this.gameObject.name + " gameobject");

        canvas = GetComponentInParent<Canvas>();
        if (canvas == null) Debug.LogError("Canvas not found from " + this.gameObject.name + " gameobject");

        graphics = GetComponentInParent<GraphicsAdoptante>();
        if (graphics == null) Debug.LogError("GraphicsAdoptante class not found from " + this.gameObject.name + " gameobject");
    }

    void Start () {
        adopterSpecie.text = adoptante.speciePreferred.ToString();
        if (adoptante.speciePreferred == Animal.ESPECIE.PAVO_REAL) adopterSpecie.text = "PAVO REAL";
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
    }

    private void OnEnable() {
        this.transform.parent = canvas.transform;
        this.transform.localPosition = Vector3.zero;
        UpdateAnimalDisplayedInfo();
    }

    private void OnDisable() {
        this.transform.parent = graphics.transform;
    }

    void UpdateAnimalDisplayedInfo() {
        if(GameLogic.instance.shelterAnimals.Count == 0) {
            Debug.Log("There is no animal in the shelter");
            return;
        }
        Animal animal = GameLogic.instance.shelterAnimals[animalId];

        animalName.text = animal.nombre;
        animalSpecie.text = animal.especie.ToString();
        if (animal.especie == Animal.ESPECIE.PAVO_REAL) animalName.text = "PAVO REAL";
        animalAge.text = animal.edad.ToString();

        switch (animal.size) {
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
    void Left() {
        animalId--;
        if (animalId < 0) {
            animalId = 0;
            return;
        }
        UpdateAnimalDisplayedInfo();
    }

    void Right() {
        animalId++;
        if (animalId >= GameLogic.instance.shelterAnimals.Count - 1) {
            animalId = GameLogic.instance.shelterAnimals.Count - 1;
            return;
        }
        UpdateAnimalDisplayedInfo();
    }
    #endregion
}
