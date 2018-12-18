using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SacrificeLogic : MonoBehaviour {
    [SerializeField] Transform animalPosition;

    private void Start() {
        GameObject animal = new GameObject();
        animal.transform.parent = animalPosition;
        animal.transform.localPosition = Vector2.zero;
        //Animal a = animal.AddComponent<Animal>();
        Animal animalSacr = GameLogic.instance.animalToSacrifice;
        print(animalSacr);
        //print(aCopy);
        //a.StartStats(aCopy.size, aCopy.edad, aCopy.confort, aCopy.estado, aCopy.especie, aCopy.hambriento, "", aCopy.color, "", aCopy.salud, aCopy.confortValue, aCopy.hambre);
        //animal.GetComponent<AnimalMovement>().enabled = false;
        GameObject g = Instantiate(GameLogic.instance.animalGraphics[(int)animalSacr.especie], animalPosition);
        TintAnimalPart[] t = g.GetComponentsInChildren<TintAnimalPart>();
        foreach(TintAnimalPart i in t){
            i.ForcePaint(animalSacr.color);
        }
    }

    public void SacrificeClick() {

    }
}
