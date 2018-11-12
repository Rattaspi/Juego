using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalChangeName : MonoBehaviour {
    Animal animalInfo;
    ShowInfoAnimal showInfo;
    [SerializeField] Text placeholder;
    [SerializeField] InputField inputField;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");
        showInfo = transform.parent.GetComponentInChildren<ShowInfoAnimal>();
        if (showInfo == null) Debug.LogError("ShowAnimalInfo script not found from AnimalChangeName");
    }

    void OnEnable () {
        placeholder.text = animalInfo.nombre;
	}

    public void ChangeName() {
        animalInfo.nombre = inputField.text;
        showInfo.UpdateInfo();
        this.gameObject.SetActive(false);
    }
}
