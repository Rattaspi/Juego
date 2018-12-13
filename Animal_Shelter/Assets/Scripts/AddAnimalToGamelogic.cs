using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimalToGamelogic : MonoBehaviour {
    Animal a;

    private void Awake() {
        a = GetComponent<Animal>();
    }

    void Start () {
        GameLogic.instance.shelterAnimals.Add(a);
	}
}
