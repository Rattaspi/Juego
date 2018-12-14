using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SelectedAnimalDisplayer : MonoBehaviour {

    public Animal selectedAnimalInList;

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI sizeText;

    public Image healthBar;
    public Image healthIcon;

    public Image ageBar;
    public Image ageIcon;

    public Image foodBar;
    public Image foodIcon;

    public GameObject selectedAnimalGraphics;

    public Image humorRepresentation;

    public Sprite caraContenta;
    public Sprite caraSeria;
    public Sprite caraTriste;

    //IEnumerator assignToCanvas() {
    //    while (CanvasScript.canvasScript == null) {
    //        yield return null;
    //    }
    //    Debug.Log("Assigned");
    //    CanvasScript.canvasScript.selectedAnimalDisplayer = this;
    //}

    public GameObject GenerateAnimal() {
        GameObject g;
        GameObject resultingObject;

        resultingObject = Instantiate(GameLogic.instance.animalGraphics[(int)selectedAnimalInList.especie]);

        resultingObject.transform.parent = gameObject.transform;
        //resultingObject.transform.localPosition = new Vector3(0, -800, 0);
        resultingObject.transform.localScale = new Vector3(1.4f, 1.4f, 1.4f);
        TintAnimalPart[] parts = resultingObject.GetComponentsInChildren<TintAnimalPart>();
        for (int i = 0; i < parts.Length; i++) {
            parts[i].ForcePaint(selectedAnimalInList.color);
        }
        return resultingObject;
    }


    public void SetInfo(Animal anAnimal) {
        if (selectedAnimalInList != anAnimal) {
            Debug.Log("Bruh");
            selectedAnimalInList = anAnimal;

            nameText.text = selectedAnimalInList.nombre;

            //sizeText.text = selectedAnimalInList.size.ToString();

            healthBar.fillAmount = selectedAnimalInList.salud / 100.0f;

            foodBar.fillAmount = selectedAnimalInList.hambre / 20.0f;

            switch (selectedAnimalInList.edad) {
                case Animal.EDAD.CACHORRO:
                    ageBar.fillAmount = 0;
                    break;
                case Animal.EDAD.JOVEN:
                    ageBar.fillAmount = 0.15f;
                    break;
                case Animal.EDAD.ADULTO:
                    ageBar.fillAmount = 0.40f;
                    break;
                case Animal.EDAD.ANCIANO:
                    ageBar.fillAmount = 0.80f;
                    break;
            }

            if (foodBar.fillAmount > 0.5f) {
                foodBar.color = Color.green;
            } else if (foodBar.fillAmount > 0.15f) {
                foodBar.color = Color.yellow;
            } else {
                foodBar.color = Color.red;
            }


            if (healthBar.fillAmount > 0.5f) {
                healthBar.color = Color.green;
            } else if (foodBar.fillAmount > 0.15f) {
                healthBar.color = Color.yellow;
            } else {
                healthBar.color = Color.red;
            }

            foodBar.fillAmount = selectedAnimalInList.hambre;

            if (selectedAnimalInList.confort == Animal.CONFORT.COMODO) {
                humorRepresentation.sprite = caraContenta;
            } else if (selectedAnimalInList.confort == Animal.CONFORT.NORMAL) {
                humorRepresentation.sprite = caraSeria;

            } else {
                humorRepresentation.sprite = caraTriste;

            }

            if (selectedAnimalGraphics != null) {
                for(int i = 0; i < selectedAnimalGraphics.transform.childCount; i++) {
                    Destroy(selectedAnimalGraphics.gameObject);
                }
            }
            //selectedAnimalGraphics = new GameObject();

            selectedAnimalGraphics = GenerateAnimal();
            selectedAnimalGraphics.transform.localPosition = (new Vector2(0, -350));
        }
    }
	// Use this for initialization
	void Start () {
        //StartCoroutine(assignToCanvas());
        caraContenta = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_0");
        caraSeria = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_1");
        caraTriste = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_2");
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
