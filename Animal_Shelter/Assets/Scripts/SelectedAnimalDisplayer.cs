using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedAnimalDisplayer : MonoBehaviour {

    public Animal selectedAnimalInList;

    public Text nameText;

    public Text sizeText;

    public Image healthBar;
    public Image healthIcon;

    public Image ageBar;
    public Image ageIcon;

    public Image foodBar;
    public Image foodIcon;

    public GameObject selectedAnimalGraphics;

    IEnumerator assignToCanvas() {
        while (CanvasScript.canvasScript == null) {
            yield return null;
        }
        CanvasScript.canvasScript.selectedAnimalDisplayer = this;
    }

    public GameObject GenerateAnimal() {
        GameObject g;
        GameObject resultingObject;
        switch (selectedAnimalInList.especie) {
            case Animal.ESPECIE.GATO:
                g = Resources.Load<GameObject>("Prefabs/Animals/Gato");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.HAMSTER:
                g = Resources.Load<GameObject>("Prefabs/Animals/Hamster");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.KOALA:
                g = Resources.Load<GameObject>("Prefabs/Animals/Koala");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.NARVAL:
                g = Resources.Load<GameObject>("Prefabs/Animals/Narval");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.PALOMA:
                g = Resources.Load<GameObject>("Prefabs/Animals/Paloma");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.PERRO:
                g = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                resultingObject = Instantiate(g, this.transform);
                break;

            case Animal.ESPECIE.TIGRE:
                g = Resources.Load<GameObject>("Prefabs/Animals/Tigre");
                resultingObject = Instantiate(g, this.transform);
                break;

            default:
                g = Resources.Load<GameObject>("Prefabs/Animals/Perro");
                resultingObject = Instantiate(g, this.transform);
                break;
        }
        resultingObject.transform.parent = gameObject.transform;
        resultingObject.transform.localPosition = new Vector3(0, 0, 0);
        TintAnimalPart[] parts = resultingObject.GetComponentsInChildren<TintAnimalPart>();
        for (int i = 0; i < parts.Length; i++) {
            parts[i].ForcePaint(selectedAnimalInList.color);
        }
        return resultingObject;
    }


    public void SetInfo(Animal anAnimal) {
        Debug.Log("Bruh");
        selectedAnimalInList = anAnimal;

        nameText.text = selectedAnimalInList.nombre;

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

        selectedAnimalGraphics = GenerateAnimal();

    }
	// Use this for initialization
	void Start () {
        StartCoroutine(assignToCanvas());
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
