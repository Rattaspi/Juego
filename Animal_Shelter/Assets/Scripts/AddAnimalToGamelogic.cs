using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAnimalToGamelogic : MonoBehaviour {
    Animal a;

    private void Awake() {
        a = GetComponent<Animal>();
        GameLogic.instance.shelterAnimals.Add(a);
        GameLogic.instance.animalToSacrifice = a;
    }
}
