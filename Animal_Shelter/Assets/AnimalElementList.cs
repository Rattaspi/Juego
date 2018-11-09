using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalElementList : MonoBehaviour {
    public RawImage animalImage; //This shall be substituted by a sprite of the actual animal, eventually.
    public Text nameText;
    public Text ageText;
    public Text statusText;
    public Text sizeText;
    public Text speciesText;
    public Button adoptButton;
    public Button rejectButton;
    public Animal associatedAnimal;

    public void AssociateAnimal(Animal animal) {
        associatedAnimal = animal;
        nameText.text = animal.nombre;
        ageText.text = animal.edad.ToString();
        statusText.text = animal.estado.ToString();
        sizeText.text = animal.size.ToString();
        speciesText.text = animal.especie.ToString();
        adoptButton.onClick.AddListener(() => AddAnimal(animal));
        rejectButton.onClick.AddListener(() => RejectAnimal(animal));
    }

    void RejectAnimal(Animal a) {
        Destroy(a.gameObject);
        Destroy(gameObject);
    }

    void AddAnimal(Animal a) {
        if (GameLogic.instance.shelterAnimals.Count < GameLogic.instance.currentAnimalCapacity) {
            a.gameObject.transform.SetParent(GameLogic.instance.animalObjectParent.transform);
            GameLogic.instance.shelterAnimals.Add(a);
            Destroy(gameObject);
        } else {
            adoptButton.GetComponentInChildren<Text>().text = "No hay sitio";
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
