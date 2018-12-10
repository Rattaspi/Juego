using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TintAnimalPart : MonoBehaviour {
    Animal animalInfo;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        //if (animalInfo == null) Debug.LogError("Animal not found from TintAnimalPart");
	}

    //Método para forzar el pintado (Se usa en los casos de preview, donde animalInfo es null)
    public void ForcePaint(Color c) {
        Image i = GetComponent<Image>();
        i.color = c;
    }

    void Start () {
        Image i = GetComponent<Image>();
        if(animalInfo!=null)
        i.color = animalInfo.color;
	}
}
