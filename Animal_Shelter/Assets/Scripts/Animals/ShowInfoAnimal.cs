using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowInfoAnimal : MonoBehaviour {
    Animal animalInfo;
    [SerializeField] Text name;
    [SerializeField] Image cara;
    [SerializeField] Image salud;
    [SerializeField] Image comida;
    [SerializeField] Image edad;
    Sprite[] faces;

	void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");

        //Get the faces sprites
        faces = new Sprite[3];
        for (int i = 0; i < 3; i++) {
            faces[i] = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_" + i);
        }

    }

    void OnEnable () {
        name.text = animalInfo.nombre;
        cara.sprite = faces[(int)animalInfo.confort];
        salud.fillAmount =  1 - Mathf.InverseLerp(0, (int)Animal.ESTADO.LENGHT - 1, (int)animalInfo.estado);
        comida.fillAmount = 0.8f;
        edad.fillAmount = 0.1f + Mathf.InverseLerp(0, (int)Animal.EDAD.LENGHT - 1, (int)animalInfo.edad);
    }
}
