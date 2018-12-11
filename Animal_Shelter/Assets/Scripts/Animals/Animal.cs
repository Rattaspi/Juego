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

    private void Start() {
        StartStats();
        this.gameObject.tag = "animal";

        //ESTO ES PARA CONVERTIR EL STRING DE NOMBRES EN LA FORMA QUE NECESITA EL ARRAY
        //ME DABA PEREZA HACERLO A MANO :D
        //string s = ""
        //s = s.Replace(" ", "\",\"");
        //print(s);
    }

    void CreateBody() {
        //CREATE THE BODY ELEMENTS
        graphics = Resources.Load<GameObject>("Prefabs/AnimalBody");
        graphics = Instantiate(graphics, this.transform);
        graphics.name = "Body";
        graphics.layer = 20;

    }
}