using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {
    public GameObject graphics;

    public enum SIZE { SMALL, MEDIUM, BIG, LENGHT }
    public enum EDAD { CACHORRO, JOVEN, ADULTO, ANCIANO, LENGHT }
    public enum CONFORT { COMODO, NORMAL, INCOMODO, LENGHT };
    public enum ESTADO { SALUDABLE, ENFERMO, MUY_ENFERMO, TERMINAL, LENGHT };
    public enum ESPECIE { NARVAL, HAMSTER, TIGRE, PERRO, GATO, KOALA, PALOMA, PAVO_REAL, RINOCERONTE, ELEFANTE, LENGHT };

    public int gastoComida;
    public int gastoMedico; //en caso de tener que tratarlo
    public int salud;
    public string nombre;
    public string descripcion;
    public Color color;

    public SIZE size;
    public EDAD edad;
    public CONFORT confort;
    public ESTADO estado;
    public ESPECIE especie;

    public void StartStats() {
        size = (SIZE)Random.Range(0, (int)SIZE.LENGHT);
        edad = (EDAD)Random.Range(0, (int)EDAD.LENGHT);
        confort = CONFORT.NORMAL;
        estado = (ESTADO)Random.Range(0, (int)ESTADO.LENGHT);
        especie = (ESPECIE)Random.Range(0, (int)ESPECIE.LENGHT);

        nombre = AnimalCommonInfo.names[Mathf.FloorToInt(Random.Range(0, AnimalCommonInfo.names.Length))];
        color = Color.HSVToRGB(Random.value, 0.5f, 0.5f);

        CreateBody();
    }

    private void Start() {
        StartStats();
        //ESTO ES PARA CONVERTIR EL STRING DE NOMBRES EN LA FORMA QUE NECESITA EL ARRAY
        //ME DABA PEREZA HACERLO A MANO :D
        //string s = "Firulais Beethoven Hachiko Laika Pongo Scooby Rex Pluto Odie Snooppy Lassie Niebla Goofy Brian Idefix Pancho Kaiser Valkiria Odín Thor Katrina Wilma Igor Aquiles Troya Atreo Goku Akira Sayuri Chiyo Hiroki Kayoko Mitsuki Eros Laska Malak Maitea Adonis Beauty Linda Sinatra Madonna Jackson Cesar Elvis R2D2 Auro Bruc Chester Larry Lambert Milu Morgan Newman Noah Paco Dingo Casper Kira Blanca Indio Aria";
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