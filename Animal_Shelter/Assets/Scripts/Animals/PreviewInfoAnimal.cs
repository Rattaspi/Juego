using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PreviewInfoAnimal : MonoBehaviour {
    Animal animalInfo;
    [SerializeField] Text displayName;
    [SerializeField] Image happiness;
    [SerializeField] Image injured;
    [SerializeField] Sprite[] faces;

    void Awake () {
        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");

        //Get the faces sprites
        faces = new Sprite[3];
        for(int i = 0; i<3; i++) {
            faces[i] = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_" + i);
        }
    }

    private void OnEnable() {
        displayName.text = animalInfo.nombre;
        happiness.sprite = faces[(int)animalInfo.confort];
        if (animalInfo.estado != Animal.ESTADO.SALUDABLE) injured.gameObject.SetActive(true);
        else injured.gameObject.SetActive(false);
    }
}
