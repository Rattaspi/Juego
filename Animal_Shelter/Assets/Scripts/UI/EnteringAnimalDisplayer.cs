using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnteringAnimalDisplayer : MonoBehaviour {

    public Animal selectedAnimalInList;

    public Text nameText;

    public Text sizeText;
    public Text descriptionText;


    public Image healthBar;
    public Image healthIcon;

    public Image ageBar;
    public Image ageIcon;

    public Image foodBar;
    public Image foodIcon;

    public GameObject backgroundObject;

    IEnumerator AssignToCanvas() {
        while (CanvasScript.canvasScript == null) {
            yield return null;
        }
        CanvasScript.canvasScript.enteringAnimalDisplayer = this;
        backgroundObject.SetActive(false);
    }

    

    public void SetInfo(Animal anAnimal) {

        selectedAnimalInList = anAnimal;

        nameText.text = selectedAnimalInList.nombre;

        descriptionText.text = selectedAnimalInList.descripcion;

        //sizeText.text = selectedAnimalInList.size.ToString();

        healthBar.fillAmount = selectedAnimalInList.salud;

        switch (selectedAnimalInList.edad) {
            case Animal.EDAD.CACHORRO:
                ageBar.fillAmount = 0;
                break;
            case Animal.EDAD.JOVEN:
                ageBar.fillAmount = 15;
                break;
            case Animal.EDAD.ADULTO:
                ageBar.fillAmount = 40;
                break;
            case Animal.EDAD.ANCIANO:
                ageBar.fillAmount = 80;
                break;
        }

        foodBar.fillAmount = selectedAnimalInList.hambre;

    }
    // Use this for initialization
    void Start() {
        StartCoroutine(AssignToCanvas());
    }

    // Update is called once per frame
    void Update() {

    }
}
