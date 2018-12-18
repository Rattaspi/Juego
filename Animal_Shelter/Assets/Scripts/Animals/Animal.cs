using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Animal : MonoBehaviour {
    public GameObject graphics;
    public AnimalGraphics animalGraphics;

    public enum SIZE { SMALL, MEDIUM, BIG, LENGTH }
    public enum EDAD { CACHORRO, JOVEN, ADULTO, ANCIANO, LENGTH }
    public enum CONFORT { COMODO, NORMAL, INCOMODO, LENGTH };
    public enum ESTADO { SALUDABLE, ENFERMO, MUY_ENFERMO, TERMINAL, LENGTH };
    public enum ESPECIE { NARVAL, HAMSTER, TIGRE, PERRO, GATO, KOALA, PALOMA, RINOCERONTE, ELEFANTE, LENGTH };

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
        confort = (CONFORT)Random.Range(0, (int)CONFORT.LENGTH);
        estado = (ESTADO)Random.Range(0, (int)ESTADO.LENGTH);
        especie = (ESPECIE)Random.Range(0, (int)ESPECIE.LENGTH);
        hambriento = Random.Range(0, 10) >= 4;

        nombre = AnimalCommonInfo.names[Mathf.FloorToInt(Random.Range(0, AnimalCommonInfo.names.Length))];
        color = Color.HSVToRGB(Random.value, 0.8f, 0.8f);

        CreateBody();
        CreateOrigin();
        //START THE INTERNAL VALUES
        //salud [0, 100]
        ComputeNewState();

        switch (size) {
            case SIZE.SMALL:
                gastoComida = 2;
                break;
            case SIZE.MEDIUM:
                gastoComida = 4;
                break;
            case SIZE.BIG:
                gastoComida = 6;
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

    public void FeedAnimal() {
        hambre += 10;
        if (hambre > 30) {
            hambre = 30;
        }
    }

    public void TryHealing() {
        int randomChance = Random.Range(1, 100);
        switch (estado) {
            case ESTADO.SALUDABLE:
                salud += 20;
                if (salud > 100) {
                    salud = 100;
                }
                break;
            case ESTADO.ENFERMO:
                if (randomChance <= 60) {
                    estado = ESTADO.SALUDABLE;
                    ComputeNewState();
                }
                break;
            case ESTADO.MUY_ENFERMO:
                if (randomChance <= 5) {
                    estado = ESTADO.SALUDABLE;
                    ComputeNewState();
                } else if (randomChance <= 30) {
                    estado = ESTADO.ENFERMO;
                    ComputeNewState();
                }
                break;
            case ESTADO.TERMINAL:
                if (randomChance <= 2) {
                    estado = ESTADO.SALUDABLE;
                    ComputeNewState();
                } else if (randomChance <= 5) {
                    estado = ESTADO.ENFERMO;
                    ComputeNewState();
                } else if (randomChance <= 15) {
                    estado = ESTADO.MUY_ENFERMO;
                    ComputeNewState();
                }
                break;
        }

    }

    void ComputeNewState() {
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
            default:
                break;
        }
    }

    public void StartStats(SIZE size, EDAD edad, CONFORT confort, ESTADO estado, ESPECIE especie, bool hambriento, string nombre, Color color, string origin, int salud, int confortValue, int hambre) {
        this.size = size;
        this.edad = edad;
        this.confort = confort;
        this.estado = estado;
        this.especie = especie;
        this.hambriento = hambriento;
        this.nombre = nombre;
        this.color = color;
        CreateBody();
        this.descripcion = origin;
        this.salud = salud;
        this.confortValue = confortValue;
        this.hambre = hambre;
    }

    public void CreateOrigin() {
        int randomInt = Random.Range(0, Stories.inicio.Length - 1);
        descripcion = "" + Stories.inicio[randomInt];
        randomInt = Random.Range(0, Stories.nudo.Length - 1);
        descripcion += Stories.nudo[randomInt];

        randomInt = Random.Range(0, Stories.desenlace.Length - 1);
        descripcion += Stories.desenlace[randomInt];

    }

    void GetWorse() {
        int variacionSalud = 4;

        switch (estado) {
            case ESTADO.SALUDABLE:
                salud -= variacionSalud;

                break;
            case ESTADO.ENFERMO:
                salud -= variacionSalud * 2;
                break;
            case ESTADO.MUY_ENFERMO:
                salud -= variacionSalud;
                break;
        }
    }

    public void UpdateStats() {
        int variacionSalud = 4;
        int variacionConfort = 2;

        if (hambriento) {
            confortValue -= variacionConfort;
            salud -= variacionSalud;
        }
        if (estado != ESTADO.SALUDABLE) {
            int randomChanceToGetWorse = Random.Range(1, 100);
            switch (estado) {
                case ESTADO.SALUDABLE:
                    if (randomChanceToGetWorse < confortValue/20*100) {
                        GetWorse();
                    }
                    break;
                case ESTADO.ENFERMO:
                    salud -= variacionSalud;
                    if(confort != CONFORT.COMODO) {
                        if (randomChanceToGetWorse < 40) {
                            GetWorse();
                        }
                    } else {
                        if (randomChanceToGetWorse < 20) {
                            GetWorse();
                        }
                    }
                    break;
                case ESTADO.MUY_ENFERMO:
                    salud -= variacionSalud * 2;
                    if (confort != CONFORT.COMODO) {
                        if (randomChanceToGetWorse < 50) {
                            GetWorse();
                        }
                    } else {
                        if (randomChanceToGetWorse < 30) {
                            GetWorse();
                        }
                    }
                    break;
                case ESTADO.TERMINAL:
                    salud -= variacionSalud * 3;
                    break;
            }
            confort -= variacionConfort * 2;
        }
        if (estado == ESTADO.SALUDABLE && !hambriento) {
            confortValue += variacionConfort;
        }
        if (confort == CONFORT.INCOMODO) {
            salud -= variacionSalud;
        }

        hambre -= gastoComida;

        if (!GameLogic.instance.cleanedUpThisWeek) {
            confort -= confortValue*3;
        }

        if (!GameLogic.instance.payedExpensesThisWeek) {
            confort -= confortValue*2;
        }


    }

    public static GameObject MakeARandomAnimal() {
        GameObject animalObject = new GameObject();
        animalObject.transform.SetParent(CanvasScript.canvasScript.tempAnimalParent.transform);
        Animal temp2;
        temp2 = animalObject.AddComponent<Animal>();
        temp2.StartStats();
        //Debug.Log(temp2);
        animalObject.name = temp2.nombre;



        //animalObject.transform.position 
        return animalObject;
    }

    private void Awake() {
        //StartStats();
    }

    private void Start() {
        //StartStats();
        this.gameObject.tag = "animal";
        if (TutorialOverrider.instance == null) {
            this.gameObject.AddComponent<AnimalMovement>();
        }
        Rigidbody2D rb = this.gameObject.AddComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;

        BoxCollider2D b = this.gameObject.AddComponent<BoxCollider2D>();
        b.size = new Vector2(100, 200);
        b.isTrigger = true;

        //ESTO ES PARA CONVERTIR EL STRING DE NOMBRES EN LA FORMA QUE NECESITA EL ARRAY
        //ME DABA PEREZA HACERLO A MANO :D
        //string s = ""
        //s = s.Replace(" ", "\",\"");
        //print(s);
    }

    private void Update() {
        //if (Input.GetKeyDown(KeyCode.Space)) DisableOnClickingAway(true);
    }

    void CreateBody() {
        //CREATE THE BODY ELEMENTS
        transform.localPosition = Vector3.zero;
        graphics = Resources.Load<GameObject>("Prefabs/AnimalBody");
        graphics = Instantiate(graphics, this.transform);
        graphics.name = "Body";
        graphics.layer = 20;

        animalGraphics = GetComponentInChildren<AnimalGraphics>();
        if (animalGraphics == null) Debug.LogError("AnimalGraphics not found from " + this.name);

        DisableOnClickingAway(true);
    }

    public void DisableOnClickingAway(bool b) {
        animalGraphics.clickAwayDisables = b;
    }

    public void UpdateDisplayedAnimalInfo() {
        animalGraphics.showInfo.UpdateInfo();
    }
}