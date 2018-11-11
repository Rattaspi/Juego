using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoAnimal : MonoBehaviour {
    Animal animalInfo;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");
	}
	
	void Update () {
		
	}
}
