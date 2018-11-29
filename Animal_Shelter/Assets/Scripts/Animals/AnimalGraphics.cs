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

    [SerializeField] PreviewInfoAnimal previewInfo;
    [SerializeField] ShowInfoAnimal showInfo;

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

        info.SetActive(false);
        changeName.SetActive(false);

        GenerateAnimal();
    }

    private void GenerateAnimal() {
        GameObject g;
        switch (animalInfo.especie) {
            case Animal.ESPECIE.GATO:
                g = Resources.Load<GameObject>("Prefabs/Animals/Gato");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.HAMSTER:
                g = Resources.Load<GameObject>("Prefabs/Animals/Hamster");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.KOALA:
                g = Resources.Load<GameObject>("Prefabs/Animals/Koala");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.NARVAL:
                g = Resources.Load<GameObject>("Prefabs/Animals/Narval");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.PALOMA:
                g = Resources.Load<GameObject>("Prefabs/Animals/Paloma");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.PERRO:
                g = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.TIGRE:
                g = Resources.Load<GameObject>("Prefabs/Animals/Tigre");
                Instantiate(g, this.transform);
                break;

            default:
                g = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                Instantiate(g, this.transform);
                break;
        }
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
        else if (Input.GetKey(KeyCode.Mouse0) && showInfo.gameObject.activeInHierarchy) {
            pointerEvent = new PointerEventData(eventSystem);
            pointerEvent.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();
            graphicRaycaster.Raycast(pointerEvent, results);
            print(results.Count);

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
        previewInfo.gameObject.SetActive(false);
        showInfo.gameObject.SetActive(true);
    }
}
