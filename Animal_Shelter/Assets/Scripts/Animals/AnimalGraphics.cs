using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AnimalGraphics : MonoBehaviour {
    GameObject info; //object containing preview info and complete info
    public GameObject changeName;
    Animal animalInfo;
    RectTransform rTransform;

    [SerializeField] PreviewInfoAnimal previewInfo;
    [HideInInspector] public ShowInfoAnimal showInfo;
    [HideInInspector] public bool clickAwayDisables = true;

    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;
    PointerEventData pointerEvent;

    void Start () {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (graphicRaycaster == null) Debug.LogError("Graphic raycaster not found from AnimalGraphics.cs");
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null) Debug.LogError("EventSystem not found from AnimalGraphics.cs");

        info = Resources.Load<GameObject>("Prefabs/AnimalInfoPreview");
        info = Instantiate(info, this.transform);

        changeName = Resources.Load<GameObject>("Prefabs/ChangeName");
        changeName = Instantiate(changeName, this.transform);
        
        previewInfo = GetComponentInChildren<PreviewInfoAnimal>();
        showInfo = GetComponentInChildren<ShowInfoAnimal>();
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal.cs not found from AnimalGraphics.cs");

        rTransform = GetComponent<RectTransform>();

        info.SetActive(false);
        changeName.SetActive(false);

        GenerateAnimal();

        //Change display size depending on the animal size.
        switch (animalInfo.size) {
            case Animal.SIZE.SMALL:
                rTransform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                break;
            case Animal.SIZE.MEDIUM:
                rTransform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                break;
            case Animal.SIZE.BIG:
                rTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                break;
        }
    }

    private void GenerateAnimal() {
        Instantiate(GameLogic.instance.animalGraphics[(int)animalInfo.especie], this.transform);
    }

    private void Update() {
        //IN CHANGE NAME SCREEN
        //when clicking outside change name screen it closes
        if (Input.GetKey(KeyCode.Mouse0) && changeName.activeInHierarchy) {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEvent, results);

            if (results[0].gameObject.tag == "graphic_background") {
                changeName.SetActive(false);
            }
        }

        //when show info is active and click anyway but the info sprite it gets disabled
        else if (Input.GetKey(KeyCode.Mouse0) && showInfo.gameObject.activeInHierarchy && clickAwayDisables) {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEvent, results);

            if(results.Count == 0) {
                info.SetActive(false);
            }
            else if(results[0].gameObject.tag != "animal_info") {
                if (results[0].gameObject.tag == "animal" && !results[0].gameObject.GetComponent<AnimalGraphics>().animalInfo.Equals(animalInfo)) {
                    info.SetActive(false);
                }
            }
        }
    }

    public void ActivatePreview(bool b) {
        if (!showInfo.gameObject.activeInHierarchy) {
            if (b) {
                info.SetActive(b);
                previewInfo.gameObject.SetActive(true);
                showInfo.gameObject.SetActive(false);
            }
            else {
                info.SetActive(false);
            }
        }
    }

    public void ActivateInfo() {
        if (TutorialOverrider.instance != null) {
            TutorialOverrider.instance.GoToNextEvent();
        }
        GetComponent<ScaleReactor>().React();
        previewInfo.gameObject.SetActive(false);
        showInfo.gameObject.SetActive(true);
    }
}
