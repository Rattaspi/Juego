using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
public class AnimalElementList : MonoBehaviour {
    //public RawImage animalImage; //This shall be substituted by a sprite of the actual animal, eventually.
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI ageText;
    public TextMeshProUGUI statusText;
    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI speciesText;
    //public Button adoptButton;
    //public Button rejectButton;
    public Animal associatedAnimal;
    GameObject graphicsObject;

    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;
    PointerEventData pointerEvent;

    public void AssociateAnimal(Animal animal) {
        associatedAnimal = animal;
        nameText.text = animal.nombre;
        ageText.text = animal.edad.ToString();
        statusText.text = animal.estado.ToString();
        sizeText.text = animal.size.ToString();
        speciesText.text = animal.especie.ToString();
        graphicsObject = Instantiate(GameLogic.instance.animalGraphics[(int)animal.especie]);
        graphicsObject.transform.parent = gameObject.transform;
        graphicsObject.transform.localPosition = new Vector3(-180, 0, 0);
        graphicsObject.transform.localScale = new Vector3(0.5f, 0.5f,0.5f);


        TintAnimalPart[] parts = graphicsObject.GetComponentsInChildren<TintAnimalPart>();
        for (int i = 0; i < parts.Length; i++) {
            parts[i].ForcePaint(animal.color);
        }

        //adoptButton.onClick.AddListener(() => AddAnimal(animal));
        //rejectButton.onClick.AddListener(() => RejectAnimal(animal));
    }

    void RejectAnimal(Animal a) {
        Destroy(a.gameObject);
        Destroy(gameObject);
    }

    void AddAnimal(Animal a) {
        if (GameLogic.instance.shelterAnimals.Count < GameLogic.instance.currentAnimalCapacity) {
            a.gameObject.transform.SetParent(GameLogic.instance.animalObjectParent.transform);
            GameLogic.instance.shelterAnimals.Add(a);
            Destroy(gameObject);
        } else {
            //adoptButton.GetComponentInChildren<Text>().text = "No hay sitio";
        }
    }

	// Use this for initialization
	void Start () {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (graphicRaycaster == null) Debug.LogError("Graphic raycaster not found from AnimalGraphics.cs");
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null) Debug.LogError("EventSystem not found from AnimalGraphics.cs");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Mouse0)) {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEvent, results);


            if (results.Count > 0) {
                if (results[0].gameObject.tag == "animalElementList") {
                    CanvasScript.instance.SelectAnimal(results[0].gameObject.GetComponent<AnimalElementList>());
                    //Debug.Log(results[0].gameObject.GetComponentInParent<AnimalElementList>());
                    //Debug.Log(results[0].gameObject);
                }
            }

        }
    }
}
