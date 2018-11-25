using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalChangeName : MonoBehaviour {
    Animal animalInfo;
    ShowInfoAnimal showInfo;
    AnimalGraphics body;
    Canvas mainCanvas;
    [SerializeField] Text placeholder;
    [SerializeField] InputField inputField;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");
        showInfo = transform.parent.GetComponentInChildren<ShowInfoAnimal>();
        if (showInfo == null) Debug.LogError("ShowAnimalInfo script not found from AnimalChangeName");
        mainCanvas = GetComponentInParent<Canvas>();
        if (mainCanvas == null) Debug.LogError("Canvas not foundd from AnimalChangeName");
        body = GetComponentInParent<AnimalGraphics>();
        if (body == null) Debug.LogError("AnimalGraphics not found from AnimalChangeName");
    }

    void OnEnable () {
        this.transform.parent = mainCanvas.transform;
        this.transform.position = Vector3.zero;
        placeholder.text = animalInfo.nombre;
        inputField.text = "";
	}

    private void OnDisable() {
        this.transform.parent = body.transform;
    }

    public void ChangeName() {
        animalInfo.nombre = inputField.text;
        showInfo.UpdateInfo();
        this.gameObject.SetActive(false);
    }
}
