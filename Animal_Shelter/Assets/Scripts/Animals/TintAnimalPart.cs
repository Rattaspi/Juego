using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TintAnimalPart : MonoBehaviour {
    Animal animalInfo;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal not found from TintAnimalPart");
	}
	
	void Start () {
        Image i = GetComponent<Image>();
        i.color = animalInfo.color;
	}
}
