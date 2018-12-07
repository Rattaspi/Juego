using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {
    public GameObject graphics;

    public enum SIZE { SMALL, MEDIUM, BIG, LENGTH }
    public enum EDAD { CACHORRO, JOVEN, ADULTO, ANCIANO, LENGTH }
    public enum CONFORT { COMODO, NORMAL, INCOMODO, LENGTH };
    public enum ESTADO { SALUDABLE, ENFERMO, MUY_ENFERMO, TERMINAL, LENGTH };
    public enum ESPECIE { NARVAL, HAMSTER, TIGRE, PERRO, GATO, KOALA, PALOMA, PAVO_REAL, RINOCERONTE, ELEFANTE, LENGTH };

    public int gastoComida;
    public int gastoMedico; //en caso de tener que tratarlo
    public int salud;
    public int confortValue;
    public int hambre;
    public string nombre;
    public string descripcion;
    public Color color;

    public SIZE size;
    public EDAD edad;
    public CONFORT confort;
    public ESTADO estado;
    public ESPECIE especie;
    public bool hambriento;

    public void StartStats() {
        size = (SIZE)Random.Range(0, (int)SIZE.LENGTH);
        edad = (EDAD)Random.Range(0, (int)EDAD.LENGTH);
        confort = CONFORT.NORMAL;
        estado = (ESTADO)Random.Range(0, (int)ESTADO.LENGTH);
        especie = (ESPECIE)Random.Range(0, (int)ESPECIE.LENGTH);
        hambriento = Random.Range(0, 10) >= 4;

        nombre = AnimalCommonInfo.names[Mathf.FloorToInt(Random.Range(0, AnimalCommonInfo.names.Length))];
        color = Color.HSVToRGB(Random.value, 0.8f, 0.8f);

        CreateBody();
        CreateOrigin();
        //START THE INTERNAL VALUES
        //salud [0, 100]
        switch (estado) {
            case ESTADO.SALUDABLE:
                salud = Random.Range(46, 80);
                break;

            case ESTADO.ENFERMO:
                salud = Random.Range(21, 45);
                break;

            case ESTADO.MUY_ENFERMO:
                salud = Random.Range(11, 20);
                break;

            case ESTADO.TERMINAL:
                salud = Random.Range(5, 10);
                break;
        }

        //confort [0,20]
        switch (confort) {
            case CONFORT.COMODO:
                confortValue = Random.Range(16, 20);
                break;

            case CONFORT.NORMAL:
                confortValue = Random.Range(6, 15);
                break;

            case CONFORT.INCOMODO:
                confortValue = Random.Range(0, 5);
                break;
        }
        hambre = hambriento ? Random.Range(0, 9) : Random.Range(10, 20);
    }

    public void CreateOrigin() {
        int randomInt = Random.Range(0, Stories.inicio.Length - 1);
        descripcion = "" + Stories.inicio[randomInt];
        randomInt = Random.Range(0, Stories.nudo.Length - 1);
        descripcion += Stories.nudo[randomInt]; 

        randomInt = Random.Range(0, Stories.desenlace.Length - 1);
        descripcion += Stories.desenlace[randomInt];

    }

    public void UpdateStats() {
        int variacionSalud = 5;
        int variacionConfort = 2;

        if (hambriento) {
            confortValue -= variacionConfort;
            salud -= variacionSalud;
        }
        if(estado != ESTADO.SALUDABLE) {
            confort -= variacionConfort;
        }
        if(estado == ESTADO.SALUDABLE && !hambriento) {
            confortValue += variacionConfort;
        }
        if(confort == CONFORT.INCOMODO) {
            salud -= variacionSalud;
        }
    }

    public static GameObject MakeARandomAnimal() {
        GameObject animalObject = new GameObject();
        animalObject.transform.SetParent(CanvasScript.canvasScript.tempAnimalParent.transform);
        Animal temp2;
        temp2 = animalObject.AddComponent<Animal>();
        temp2.StartStats();
        //Debug.Log(temp2);
        animalObject.name = temp2.name;
        return animalObject;
    }

    private void Start() {
        StartStats();
        this.gameObject.tag = "animal";

        //ESTO ES PARA CONVERTIR EL STRING DE NOMBRES EN LA FORMA QUE NECESITA EL ARRAY
        //ME DABA PEREZA HACERLO A MANO :D
        //string s = "Firulais Beethoven Hachiko Laika Pongo Scooby Rex Pluto Odie Snooppy Lassie Niebla Goofy Brian Idefix Pancho Kaiser Valkiria Odín Thor Katrina Wilma Igor Aquiles Troya Atreo Goku Akira Sayuri Chiyo Hiroki Kayoko Mitsuki Eros Laska Malak Maitea Adonis Beauty Linda Sinatra Madonna Jackson Cesar Elvis R2D2 Auro Bruc Chester Larry Lambert Milu Morgan Newman Noah Paco Dingo Casper Kira Blanca Indio Aria";
        //s = s.Replace(" ", "\",\"");
        //print(s);
    }

    void CreateBody() {
        //CREATE THE BODY ELEMENTS
        //graphics = Resources.Load<GameObject>("Prefabs/AnimalBody");
        //graphics = Instantiate(graphics, this.transform);
        //graphics.name = "Body";
        //graphics.layer = 20;

    }
}