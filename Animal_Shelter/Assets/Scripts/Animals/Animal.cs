using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {
    public enum SIZE { SMALL, MEDIUM, BIG}
    public enum EDAD { CACHORRO, JOVEN, ADULTO, ANCIANO}
    public enum CONFORT { COMODO, NORMAL, INCOMODO};
    public enum ESTADO { SALUDABLE, ENFERMO, MUY_ENFERMO, TERMINAL};
    public enum ESPECIE { NARVAL, HAMSTER, TIGRE, PERRO, GATO, KOALA, PALOMA, PAVO_REAL, RINOCERONTE, ELEFANTE};

    [SerializeField] int gastoComida;
    [SerializeField] int gastoMedico; //en caso de tener que tratarlo
    [SerializeField] int salud;
    [SerializeField] string nombre;
    [SerializeField] string descripcion;
    [SerializeField] Color color;

    [SerializeField] SIZE size;
    [SerializeField] EDAD edad;
    [SerializeField] CONFORT confort;
    [SerializeField] ESTADO estado;
    [SerializeField] ESPECIE especie;

    private void Start() {
        size = (SIZE)Random.Range(0, )