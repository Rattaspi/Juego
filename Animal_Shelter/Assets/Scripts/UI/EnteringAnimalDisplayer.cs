using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnteringAnimalDisplayer : MonoBehaviour {

    public Animal selectedAnimalInList;

    public TextMeshProUGUI nameText;

    public TextMeshProUGUI sizeText;
    public TextMeshProUGUI descriptionText;


    public Image healthBar;
    public Image healthIcon;

    public Image ageBar;
    public Image ageIcon;

    public Image foodBar;
    public Image foodIcon;

    public Image humorRepresentation;

    public GameObject backgroundObject;
    public GameObject backgroundForAnimalImageObject;
    public GameObject currentAnimalPreview;

    public Sprite caraContenta;
    public Sprite caraSeria;
    public Sprite caraTriste;

    public float prevHorizontalAxis;

    IEnumerator AssignToCanvas() {



        while (CanvasScript.canvasScript == null) {
            yield return null;
        }
        CanvasScript.canvasScript.enteringAnimalDisplayer = this;
        backgroundObject.SetActive(false);
    }

    //Modificación del GenerateAnimal original que devuelve una referencia al GameObject que se crea y le canvia los colores de las imagenes hijas, además de hacer padre al objeto background para ya colocarlo en su sitio.
    public GameObject GenerateAnimal() {
        GameObject g;
        GameObject resultingObject;
        resultingObject = Instantiate(GameLogic.instance.animalGraphics[(int)selectedAnimalInList.especie]);
        resultingObject.transform.parent = backgroundForAnimalImageObject.transform;
        resultingObject.transform.localPosition = new Vector3(0, 0, 0);
        TintAnimalPart[] parts = resultingObject.GetComponentsInChildren<TintAnimalPart>();
        for(int i = 0; i < parts.Length; i++) {
            parts[i].ForcePaint(selectedAnimalInList.color);
        }
        return resultingObject;
    }

    public void SetInfo(Animal anAnimal) {

        selectedAnimalInList = anAnimal;

        nameText.text = selectedAnimalInList.nombre;

        descriptionText.text = selectedAnimalInList.descripcion;

        //sizeText.text = selectedAnimalInList.size.ToString();
        
        healthBar.fillAmount = selectedAnimalInList.salud/100.0f;

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

        if(selectedAnimalInList.confort== Animal.CONFORT.COMODO) {
            humorRepresentation.sprite = caraContenta;
        }else if(selectedAnimalInList.confort== Animal.CONFORT.NORMAL) {
            humorRepresentation.sprite = caraSeria;

        } else {
            humorRepresentation.sprite = caraTriste;

        }

        foodBar.fillAmount = selectedAnimalInList.hambre/20.0f;

        if (currentAnimalPreview != null) {
            Destroy(currentAnimalPreview);
        }

        currentAnimalPreview = GenerateAnimal();

        if (foodBar.fillAmount > 0.5f) {
            foodBar.color = Color.green;
        }else if (foodBar.fillAmount > 0.15f) {
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


        //if (ageBar.fillAmount < 0.5f) {
        //    ageBar.color = Color.green;
        //} else if (ageBar.fillAmount > 0.15f) {
        //    ageBar.color = Color.yellow;
        //} else {
        //    ageBar.color = Color.red;
        //}



    }
    // Use this for initialization
    void Start() {
        caraContenta = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_0");
        caraSeria = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_1");
        caraTriste = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_2");
        StartCoroutine(AssignToCanvas());
    }

    // Update is called once per frame
    void Update() {
        float horizontalAxis = Input.GetAxisRaw("Horizontal");

        if (CanvasScript.canvasScript.enteringAnimalList.Count > 0) {
            if (horizontalAxis > 0 && horizontalAxis != prevHorizontalAxis) {
                CanvasScript.canvasScript.IncreaseDisplayIndex(1);
            } else if (horizontalAxis < 0 && horizontalAxis != prevHorizontalAxis) {
                CanvasScript.canvasScript.IncreaseDisplayIndex(-1);
            }
        }


        prevHorizontalAxis = horizontalAxis;
    }
}
