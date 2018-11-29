using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class ElementListRaycaster : MonoBehaviour {


    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;
    PointerEventData pointerEvent;

    // Use this for initialization
    void Start() {
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
                    CanvasScript.canvasScript.SelectAnimal(results[0].gameObject.GetComponentInParent<AnimalElementList>());
                    //Debug.Log(results[0].gameObject.GetComponentInParent<AnimalElementList>());
                    Debug.Log(results[0].gameObject);
                }
            }

        }
    }
}
