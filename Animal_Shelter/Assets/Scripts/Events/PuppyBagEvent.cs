using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuppyBagEvent : Event {

    int amountOfPuppies;

    public PuppyBagEvent() {
        amountOfPuppies = Random.Range(3, 5);
        description = "Encuentras un saco con " + amountOfPuppies + " cachorros, no se quieren separar.";
        title = "Bolsa de Cachorritos";
        if (amountOfPuppies > (GameLogic.instance.currentAnimalCapacity - GameLogic.instance.shelterAnimals.Count)) {
            acceptMessage = "Aceptar los cachorros (No tienes espacio)";
            canBeAccepted = false;
        } else {
            acceptMessage = "Aceptar los cachorros";
        }
        declineMessage = "Rechazar a los cachorros";
    }

    public override void OnAccept() {
        base.OnAccept();
        if (GameLogic.instance != null) {
            for (int i = 0; i < amountOfPuppies; i++) {
                GameObject animalObject = new GameObject();
                Animal newAnimal = animalObject.AddComponent<Animal>();
                newAnimal.StartStats();
                newAnimal.edad = Animal.EDAD.CACHORRO;
                GameLogic.instance.shelterAnimals.Add(newAnimal);
                animalObject.name = newAnimal.nombre;
                animalObject.transform.SetParent(GameLogic.instance.animalObjectParent.transform);
            }
        }
    }

    public override void OnDeny() {
        base.OnDeny();
    }

}
