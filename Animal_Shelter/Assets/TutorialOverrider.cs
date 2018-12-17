using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialOverrider : MonoBehaviour {
    public static TutorialOverrider instance;
    public bool enabled;
    public bool displayingText;
    public bool modifyingBubble;
    public int actionCounter;
    public GameObject objetoEntradaNombre;
    public TextMeshProUGUI textoEntradaNombre;
    public int letterIndex;
    public TextMeshProUGUI textDisplayer;
    public string currentText;

    public delegate void TestDelegate();
    public TestDelegate m_methodCall;
    public GameObject bubbleObject;

    //Bool guarro para hacer do Once en los eventos de tutorial
    public bool booleanOnce;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    // Use this for initialization
    void Start() {
        actionCounter = 0;
        objetoEntradaNombre.SetActive(false);
        m_methodCall = UnFadeGame;
    }

    /*“Buenas, tú debes de ser el Nuevo dueño…”
“Por curiosidad ¿Qué nombre tienes pensado ponerle al refugio?”
-Insertar nombre
“{Nombre}… Suena genial… Quizás esta vez sea la definitiva…”
“¿Podrías encender la luz?”
“¡Espera un momento!”
“¿Me prometes que no vas a asustarte?”
“Está bien… Ya puedes encenderla.”*/

    IEnumerator ShowBubble() {
        if (!modifyingBubble) {
            modifyingBubble = true;
            while (bubbleObject.transform.localScale.x < 1) {
                Vector3 localLocalScale;
                localLocalScale = bubbleObject.transform.localScale;
                localLocalScale += new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
                bubbleObject.transform.localScale = localLocalScale;
                yield return null;
            }
            if (bubbleObject.transform.localScale.x > 1) {
                bubbleObject.transform.localScale = new Vector3(1, 1, 1);
            }
        }
    }

    IEnumerator HideBubble() {
        if (!modifyingBubble) {
            modifyingBubble = true;
            while (bubbleObject.transform.localScale.x > 0) {
                Vector3 localLocalScale;
                localLocalScale = bubbleObject.transform.localScale;
                localLocalScale -= new Vector3(Time.deltaTime * 2, Time.deltaTime * 2, Time.deltaTime * 2);
                bubbleObject.transform.localScale = localLocalScale;
                yield return null;
            }
            if (bubbleObject.transform.localScale.x < 0) {
                bubbleObject.transform.localScale = new Vector3(0, 0, 0);
            }
        }
        Invoke("ModifyingBubbleIsFalse", 0.5f);
    }

    void ModifyingBubbleIsFalse() {
        modifyingBubble = false;
    }

    IEnumerator DisplayText() {
        displayingText = true;
        while (textDisplayer.text.Length < currentText.Length) {
            bool isComa = false;
            textDisplayer.text += currentText[letterIndex];

            if (textDisplayer.text[letterIndex] == ',') {
                isComa = true;
            }

            letterIndex++;

            if (!isComa) {
                yield return new WaitForSeconds(0.05f);
            } else {
                yield return new WaitForSeconds(0.25f);
            }

        }
        Debug.Log("End");
        //displayingText = false;
    }

    void GoToNextEvent() {
        displayingText = false;
        textDisplayer.text = "";
        actionCounter++;
        letterIndex = 0;
    }

    public void UnFadeGame() {
        FaderScript.instance.unFade = true;
    }

    public void EmptyMethod() {

    }

    void PressEnterOrSpace(bool canContinue, TestDelegate Method1, TestDelegate Method2) {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {

            if (textDisplayer.text.Length >= currentText.Length && canContinue) {
                GoToNextEvent();
                Method1();
                Method2();
            } else {
                textDisplayer.text = currentText;
                letterIndex = textDisplayer.text.Length - 1;
            }
        }
    }


    void PressEnterOrSpace(bool canContinue, TestDelegate Method1) {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {

            if (textDisplayer.text.Length >= currentText.Length && canContinue) {
                GoToNextEvent();
                Method1();
            } else {
                textDisplayer.text = currentText;
                letterIndex = textDisplayer.text.Length - 1;
            }
        }
    }


    void PressEnterOrSpace(bool canContinue) {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {

            if (textDisplayer.text.Length >= currentText.Length && canContinue) {
                GoToNextEvent();
            } else {
                textDisplayer.text = currentText;
                letterIndex = textDisplayer.text.Length - 1;
            }
        }
    }

    void StartShowBubbleCoroutine() {
        StartCoroutine(ShowBubble());
    }

    void StartHideBubbleCoroutine() {
        StartCoroutine(HideBubble());
    }

    // Update is called once per frame
    void Update() {
        if (!enabled) {
            if (GameLogic.instance != null && CanvasScript.canvasScript != null) {
                enabled = true;
            }
        } else {
            switch (actionCounter) {
                case 0:
                    if (!displayingText) {
                        currentText = "Buenas, tú debes de ser el nuevo dueño…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    //if (textDisplayer.text.Length < currentText.Length) {
                    //    string currentStringToDisplay = "";
                    //    for(int i = 0; i < lettersToShow; i++) {
                    //        currentStringToDisplay += currentText[i];
                    //    }
                    //}
                    //textDisplayer.text 
                    break;
                case 1:
                    if (!displayingText) {
                        currentText = "Por curiosidad ¿Qué nombre tienes pensado ponerle al refugio ?";
                        StartCoroutine(DisplayText());
                    }
                    PressEnterOrSpace(false);

                    if (textDisplayer.text.Length >= currentText.Length) {
                        if (!objetoEntradaNombre.activeInHierarchy) {
                            objetoEntradaNombre.SetActive(true);
                        } else {
                            if (textoEntradaNombre.text.Length >= 4) {
                                if (Input.GetKeyDown(KeyCode.Return)) {
                                    objetoEntradaNombre.SetActive(false);
                                    GoToNextEvent();
                                }
                            }
                        }
                    }

                    break;
                case 2:
                    if (!displayingText) {
                        currentText = textoEntradaNombre.text + " suena genial… Quizás esta vez sea la definitiva…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 3:

                    if (!displayingText) {
                        currentText = "¿Podrías encender la luz?";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 4:

                    if (!displayingText) {
                        currentText = "¡Espera un momento!";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 5:

                    if (!displayingText) {
                        currentText = "Prométeme que no te asustarás...";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 6:

                    if (!displayingText) {
                        currentText = "Vale, ya puedes encenderla...";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;
                case 7:
                    if (!booleanOnce && !modifyingBubble) {
                        booleanOnce = true;
                        StartShowBubbleCoroutine();
                        modifyingBubble = false;
                    }

                    if (!displayingText && !modifyingBubble) {
                        currentText = "Hola, soy el inquilino más anciano de este refugio. Llevo aquí desde… Podríamos decir que desde el principio.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }


                    break;
                case 8:

                    if (!displayingText) {
                        currentText = "El nombre que me puso mi primer dueño fue Turtlellini, pero por aquí se me conoce como el anciano.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);


                    }

                    break;
                case 9:

                    if (!displayingText) {
                        currentText = "Llevo tantos años entre vosotros que he llegado a aprender vuestro lenguaje.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);

                    }

                    break;
                case 10:

                    if (!displayingText) {
                        currentText = "Cada vez que alguien toma el mando de este lugar yo me encargo de ayudarle.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;
                case 11:

                    if (!displayingText) {
                        currentText = "Solo espero que…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;
                case 12:

                    if (!displayingText) {
                        currentText = "No… No tenemos tiempo que perder, hay muchísimo trabajo por delante.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;

                case 13:

                    if (!displayingText) {
                        currentText = "Mira!Parece que un pequeñajo te ha seguido hasta aquí";
                        StartCoroutine(DisplayText());
                    } else {
                        if (textDisplayer.text.Length == currentText.Length) {
                            //Highlight y desbloqueo Lista Animales
                        }
                        //PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;

                case 14:

                    if (!displayingText) {
                        currentText = "Vaya, parece que ha tenido una vida bastante dura…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Highlight historia animal
                    }
                    break;
                case 15:
                    if (!displayingText) {
                        currentText = "Y tiene hambre!";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador hambre
                    }
                    break;
                case 16:
                    if (!displayingText) {
                        currentText = "Creo que deberíamos darle un hogar...";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador aceptar en refugio, desbloquear botón aceptar en refugio
                    }
                    break;
                case 17:

                    if (!displayingText) {
                        currentText = "Veo que vienes con unos ahorros";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                    }
                    break;
                case 18:

                    if (!displayingText) {
                        currentText = "Deberíamos empezar por comprar comida para el pequeñajo";
                        StartCoroutine(DisplayText());
                    } else {

                    }
                    break;
                case 19:
                    if (!displayingText) {

                        currentText = "Usa tu teléfono para hacer un encargo de comida";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón gestion y desbloquear botón gestión
                    }
                    break;
                case 20:
                    if (!displayingText) {

                        currentText = "Empezaremos por comprar 10 raciones, así tendremos suficiente para unos cuantos días";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar toggle poca comida y desbloquear toggle poca comida
                    }

                    break;
                case 21:
                    if (!displayingText) {

                        currentText = "Por desgracia, tardan una semana en traer los pedidos";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón semana siguiente y desbloquear botón semana siguiente
                    }
                    break;
                case 22:
                    if (!displayingText) {

                        currentText = "Vamos a esperar";
                        StartCoroutine(DisplayText());
                    } else {
                        //Esconder y des esconder la burbuja
                        //Salto de semana automatico con fadeout fadein
                    }
                    break;
                case 23:
                    if (!displayingText) {

                        currentText = "Vaya… Realmente lo está pasando bastante mal. Menos mal que el pedido ya ha llegado";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 24:
                    if (!displayingText) {

                        currentText = "Vamos, acércate a él, no tengas miedo…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar Animal y desbloquear click animal
                    }
                    break;
                case 25:
                    if (!displayingText) {

                        currentText = "Dale una ración. Se sentirá mucho mejor.";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón dar de comer y desbloquear botón dar de comer
                    }
                    break;
                case 26:
                    if (!displayingText) {

                        currentText = "Eso es! Se le ve mucho más feliz.";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador felicidad
                    }
                    break;
                case 27:
                    if (!displayingText) {

                        currentText = "Seguramente no lo hubiera conseguido sin ti";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 28:
                    if (!displayingText) {

                        currentText = "Vaya… Es extraño…";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 29:
                    if (!displayingText) {

                        currentText = "Tengo la sensación de que tenía que explicarte algo más";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 30:
                    if (!displayingText) {

                        currentText = "Eso es! ¡Casi se me olvida!";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 31:
                    if (!displayingText) {

                        currentText = "Volvamos al gestor de gastos";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton gestion gastos
                    }
                    break;
                case 32:
                    if (!displayingText) {

                        currentText = "Es el momento de ir a buscar nuevos inquilinos para el refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 33:
                    if (!displayingText) {

                        currentText = "Podríamos salir con la vieja furgoneta a buscar por las calles";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar toggle poca gasolina
                    }
                    break;
                case 34:
                    if (!displayingText) {

                        currentText = "Tenemos que intentar ahorrar un poco…";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 35:
                    if (!displayingText) {

                        currentText = "Creo que ya hemos hecho todo lo posible por esta semana";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 36:
                    if (!displayingText) {

                        currentText = "Vayamos a la siguiente";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton fin de semana
                    }
                    break;
                case 37:
                    if (!displayingText) {

                        currentText = "Vaya! Parece que alguien ha conseguido algo de dinero. Muy bien hecho! Esto nos ayudará a salir a flote…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton aceptar evento
                    }
                    break;
                case 38:
                    if (!displayingText) {

                        currentText = "Recuerda que los animales que rescatamos de las calles todavía están en la furgoneta! Vamos a echarles un vistazo";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón entrada animales
                    }
                    break;
                case 39:
                    if (!displayingText) {

                        currentText = "Vaya! Mira cuantos nuevos amigos!";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 40:
                    if (!displayingText) {

                        currentText = "Tenemos sitio de sobra, deberíamos prepararles unas camas y dejarles entrar";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botones para acoger de todos
                    }
                    break;
                case 41:
                    if (!displayingText) {

                        currentText = "Oh! Resulta que uno de ellos está algo enfermo, veamos qué podemos hacer…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar animal enfermo
                    }
                    break;
                case 42:

                    if (!displayingText) {

                        currentText = "Vamos a darle algo de medicina...";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar medecina
                    }
                    break;


                    break;
                case 43:
                    if (!displayingText) {

                        currentText = "Bien hecho! Está como una rosa";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 44:
                    if (!displayingText) {

                        currentText = "Oh vaya! Parece que tenemos visita!";
                        StartCoroutine(DisplayText());
                    } else {
                        //Generar adoptante
                        //Resaltar adoptante
                    }
                    break;
                case 45:
                    if (!displayingText) {

                        currentText = "Le echaremos de menos, pero al fin y al cabo es nuestra función, debemos cuidar de ellos hasta que encuentren un hogar mejor.Despídete de él, seguro que será muy feliz";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón entregar
                    }
                    break;
                case 46:
                    if (!displayingText) {

                        currentText = "No te pongas triste, él estaba muy agradecido por lo que has hecho por él, has hecho feliz a un animal y has hecho feliz a un humano.";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 47:
                    if (!displayingText) {

                        currentText = "Vaya! Además, parece que el adoptante ha recomendado el refugio a algunos conocidos, nuestra reputación ha aumentado.";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador reputación
                    }
                    break;
                case 48:
                    if (!displayingText) {

                        currentText = "Esto nos ayudará, haciendo que venga más gente.";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 49:
                    if (!displayingText) {

                        currentText = "¡Ahora que lo recuerdo! Podemos hacer algo más para hacer que venga gente";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 50:
                    if (!displayingText) {

                        currentText = "Vayamos de nuevo a la gestión de gastos";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton gastos
                    }
                    break;
                case 51:
                    if (!displayingText) {

                        currentText = "Vamos a hacer algo de publicidad";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar gasto mínimo de publicidad
                    }
                    break;
                case 52:
                    if (!displayingText) {

                        currentText = "Nos aseguraremos de que vengan más adoptantes durante la semana que viene";
                        StartCoroutine(DisplayText());
                    } else {
                        
                    }
                    break;
                case 53:
                    if (!displayingText) {

                        currentText = "Ten en cuenta que cuantos más inquilinos tengas, más gastos básicos tendrás durante cada semana, esto incluye cosas como la limpieza, el agua, comprar juguetes, etc";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar gasto fijo
                    }
                    break;
                case 54:
                    if (!displayingText) {

                        currentText = "Por lo que deberás tener cuidado en la forma que gastas el dinero";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                    }
                    break;
                case 55:
                    if (!displayingText) {

                        currentText = "Recuerda que deberás echar gasolina a la furgoneta para poder ir a buscar nuevos animales para el refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar toggle gasolina
                    }
                    break;
                case 56:
                    if (!displayingText) {

                        currentText = "Aunque decidas no salir a buscar animales, de vez en cuando aparecerá alguno en la puerta del refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                    }
                    break;
                case 57:
                    if (!displayingText) {

                        currentText = "Pero es probable que haya un momento que necesitemos tener una entrada frecuente de inquilinos para poder satisfacer a los adoptantes";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                    }
                    break;
                case 58:
                    if (!displayingText) {

                        currentText = "";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                    }
                    break;





            }
        }
    }
}
