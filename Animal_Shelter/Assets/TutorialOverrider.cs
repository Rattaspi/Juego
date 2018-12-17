﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class TutorialOverrider : MonoBehaviour {
    public enum ElementToHighlight { NONE,MANAGEMENT_BUTTON, MONEY_ICON, WEEK_TIMER, ENTRANCE_BUTTON, ACCEPT_BUTTON, ANIMAL_ENTRANCE, SMALL_TOGGLE_PUBLICITY, HUNGER_INDICATOR_ENTERING, SMALL_FOOD_TOGGLE, ACCEPTED_ANIMAL, FEED_BUTTON, HAPPINESS_INDICATOR, SMALL_TOGGLE_GAS, ACCEPT_EVENT_BUTTON, ALL_ENTRANCES, SICK_ANIMAL, HEAL_BUTTON, ADOPTER, GIVE_AWAY, REPUTATION, TOTAL_EXPENSE }
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

    public GameObject leftImage;
    public GameObject rightImage;
    public GameObject botImage;
    public GameObject topImage;

    public GameObject resourceManagementWindow;
    public Button animalEntranceButton;
    public Button acceptAnimalButton;
    public Button resourceManagementButton;
    public Toggle toggleGas;
    public Toggle toggleFood;
    public Toggle togglePublicity;

    //Bool guarro para hacer do Once en los eventos de tutorial
    public bool booleanOnce;

    private void Awake() {
        if (instance == null) {
            instance = this;
        }
    }

    public void SetPosWidthHeight(GameObject g, float x, float y, float z, float w, float h) {
        g.transform.localPosition = new Vector3(x, y, z);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, h);
        g.GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, w);
    }

    public void HighlightElement(ElementToHighlight h) {

        leftImage.SetActive(true);
        rightImage.SetActive(true);
        botImage.SetActive(true);
        topImage.SetActive(true);

        switch (h) {

            case ElementToHighlight.NONE:
                SetPosWidthHeight(leftImage, 0, 0, 0, 1920, 1080);
                SetPosWidthHeight(rightImage, 0, 0, 0, 0, 0);
                SetPosWidthHeight(botImage, 0, 0, 0, 0, 0);
                SetPosWidthHeight(topImage, 0, 0, 0, 0, 0);
                break;

            case ElementToHighlight.ENTRANCE_BUTTON:
                Debug.Log("Highlight");
                SetPosWidthHeight(leftImage, -173.92f, 0, 0, 1572.3f, 1080);
                SetPosWidthHeight(rightImage, 927.2f, 455.43f, 0, 65.6f, 169.15f);
                SetPosWidthHeight(botImage, 786.1099f, -84.6f, 0, 347.8f, 910.9f);
                SetPosWidthHeight(topImage, 753.3f, 523.8f, 0, 282.2f, 32.5f);


                break;
            case ElementToHighlight.ANIMAL_ENTRANCE:

                SetPosWidthHeight(leftImage, -705.0455f, 0, 0, 510.11f, 1080);
                SetPosWidthHeight(rightImage, 704.9916f, 171.98f, 0, 510.08f, 735.95f);
                SetPosWidthHeight(botImage, 255f, -368.02f, 0, 1410, 344.05f);
                SetPosWidthHeight(topImage, 0.34628f, 261.8f, 0, 899.21f, 556.4f);

                break;
            case ElementToHighlight.ACCEPT_BUTTON:

                SetPosWidthHeight(leftImage, -410, 0, 0, 1100.2f, 1080);
                SetPosWidthHeight(rightImage, 622.1f, 117.12f, 0, 675.9f, 845.73f);
                SetPosWidthHeight(botImage, 550.05f, -422.9f, 0, 819.9f, 234.3f);
                SetPosWidthHeight(topImage, 212.12f, 179f, 0, 144.1f, 722);

                break;
            case ElementToHighlight.MANAGEMENT_BUTTON:
                SetPosWidthHeight(leftImage, -454.9f, 0, 0, 1010.5f, 1080f);
                SetPosWidthHeight(rightImage, 608.3f, 0, 0, 703.3f, 1080);
                SetPosWidthHeight(botImage, 153.5f, -529.2f, 0, 206.3f, 21.8f);
                SetPosWidthHeight(topImage, 153.51f, 109.8f, 0, 206.29f, 860.5f);

                break;
            case ElementToHighlight.MONEY_ICON:
                SetPosWidthHeight(leftImage, -927.9f, 0, 0, 64.6f, 1080f);
                SetPosWidthHeight(rightImage, 204.6f, 438.44f, 0, 1510.7f, 103.18f);
                SetPosWidthHeight(botImage, 32.19998f, -76.6f, 0, 1855.6f, 926.9f);
                SetPosWidthHeight(topImage, 32.16998f, 515.03f, 0, 1855.6f, 50);
                break;
            case ElementToHighlight.WEEK_TIMER:
                SetPosWidthHeight(leftImage, -162.2f, 0, 0, 1596, 1080f);
                SetPosWidthHeight(rightImage, 941.8f, -25, 0, 36.3f, 1030.1f);
                SetPosWidthHeight(botImage, 779.72f, -526.1f, 0, 287.85f, 27.8f);
                SetPosWidthHeight(topImage, 779.73f, 157.1f, 0, 287.84f, 765.8f);
                break;

            case ElementToHighlight.SMALL_TOGGLE_PUBLICITY:

                SetPosWidthHeight(leftImage, -597.1f, 0, 0, 726.3f, 1080f);
                SetPosWidthHeight(rightImage, 470.21f, -187, 0, 979.5f, 706);
                SetPosWidthHeight(botImage, -126.74f, -256.8f, 0, 214.4f, 566.5f);
                SetPosWidthHeight(topImage, 363.02f, 353, 0, 1193.9f, 374);

                break;
            case ElementToHighlight.HUNGER_INDICATOR_ENTERING:

                SetPosWidthHeight(leftImage, -661, 0, 0, 598.7f, 1080);
                SetPosWidthHeight(rightImage, 470.7f, 0, 0, 978.6f, 1080);
                SetPosWidthHeight(botImage, -190.13f, -238.5f, 0, 343, 603.6f);
                SetPosWidthHeight(topImage, -190.14f, 334.5f, 0, 343, 411.1f);

                break;
            case ElementToHighlight.SMALL_FOOD_TOGGLE:

                
                SetPosWidthHeight(leftImage, -597.09f, 0, 0, 726.32f, 1080);
                SetPosWidthHeight(rightImage, 470.21f, -84.59998f, 0, 979.5f, 910.8f);
                SetPosWidthHeight(botImage, -126.74f, -148.3f, 0, 214.4f, 783.5f);
                SetPosWidthHeight(topImage, 363.02f, 455.4f, 0, 1193.9f, 169.2f);

                break;
            case ElementToHighlight.ACCEPTED_ANIMAL:

                //SetPosWidthHeight(leftImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(rightImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(botImage, 0, 0, 0, 0, 0);
                //SetPosWidthHeight(topImage, 0, 0, 0, 0, 0);

                break;
            case ElementToHighlight.FEED_BUTTON:

                //SetPosWidthHeight(leftImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(rightImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(botImage, 0, 0, 0, 0, 0);
                //SetPosWidthHeight(topImage, 0, 0, 0, 0, 0);

                break;
            case ElementToHighlight.HAPPINESS_INDICATOR:

                //SetPosWidthHeight(leftImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(rightImage, 0, 0, 0, 0, 1080);
                //SetPosWidthHeight(botImage, 0, 0, 0, 0, 0);
                //SetPosWidthHeight(topImage, 0, 0, 0, 0, 0);

                break;
            case ElementToHighlight.SMALL_TOGGLE_GAS:

                SetPosWidthHeight(leftImage, -597.09f, 0, 726.32f, 0, 1080);
                SetPosWidthHeight(rightImage, 470.21f, -84.59998f, 0, 979.5f, 910.8f);
                SetPosWidthHeight(botImage, -126.74f, -374.5f, 0, 214.4f, 332);
                SetPosWidthHeight(topImage, 363.02f, 241.5f, 0, 1193.9f, 597.1f);

                break;
            case ElementToHighlight.ACCEPT_EVENT_BUTTON:

                SetPosWidthHeight(leftImage, -702.5f, 0, 0, 515.6f, 1080);
                SetPosWidthHeight(rightImage, 399.22f, 0, 0, 1121.49f, 1080);
                SetPosWidthHeight(botImage, -303.11f, -411.5f, 0, 283.17f, 257.7f);
                SetPosWidthHeight(topImage, -303.12f, 167.2f, 0, 283.18f, 745.8f);

                break;
            case ElementToHighlight.ALL_ENTRANCES:
                break;
            case ElementToHighlight.SICK_ANIMAL:
                break;
            case ElementToHighlight.HEAL_BUTTON:
                break;
            case ElementToHighlight.ADOPTER:
                break;
            case ElementToHighlight.GIVE_AWAY:
                break;
            case ElementToHighlight.REPUTATION:

                SetPosWidthHeight(leftImage, -940.4f, 0, 0, 39.7f, 1080);
                SetPosWidthHeight(rightImage, 184.5f, 0, 0, 1550.9f, 1080);
                SetPosWidthHeight(botImage, -755.75f, -529, 0, 329.6f, 23);
                SetPosWidthHeight(topImage, -755.75f, 115, 0, 329.6f, 850.2f);

                break;
            case ElementToHighlight.TOTAL_EXPENSE:

                SetPosWidthHeight(leftImage, -509.7f, 0, 0, 901, 1080);
                SetPosWidthHeight(rightImage, 567.8f, 0, 0, 784.3f, 1080);
                SetPosWidthHeight(botImage, 58.22498f, -522.3f, 0, 234.85f, 36.2f);
                SetPosWidthHeight(topImage, 58.225f, 45.1f, 0, 234.9f, 990);

                break;
        }
        if (h == ElementToHighlight.NONE) {
            leftImage.GetComponent<Image>().color = new Color(leftImage.GetComponent<Image>().color.r, leftImage.GetComponent<Image>().color.g, leftImage.GetComponent<Image>().color.b,0);
            rightImage.GetComponent<Image>().color = new Color(rightImage.GetComponent<Image>().color.r, rightImage.GetComponent<Image>().color.g, rightImage.GetComponent<Image>().color.b, 0);
            botImage.GetComponent<Image>().color = new Color(botImage.GetComponent<Image>().color.r, botImage.GetComponent<Image>().color.g, botImage.GetComponent<Image>().color.b, 0);
            topImage.GetComponent<Image>().color = new Color(topImage.GetComponent<Image>().color.r, topImage.GetComponent<Image>().color.g, topImage.GetComponent<Image>().color.b, 0);
        } else {
            leftImage.GetComponent<Image>().color = new Color(leftImage.GetComponent<Image>().color.r, leftImage.GetComponent<Image>().color.g, leftImage.GetComponent<Image>().color.b, 0.7215686f);
            rightImage.GetComponent<Image>().color = new Color(rightImage.GetComponent<Image>().color.r, rightImage.GetComponent<Image>().color.g, rightImage.GetComponent<Image>().color.b, 0.7215686f);
            botImage.GetComponent<Image>().color = new Color(botImage.GetComponent<Image>().color.r, botImage.GetComponent<Image>().color.g, botImage.GetComponent<Image>().color.b, 0.7215686f);
            topImage.GetComponent<Image>().color = new Color(topImage.GetComponent<Image>().color.r, topImage.GetComponent<Image>().color.g, topImage.GetComponent<Image>().color.b, 0.7215686f);
        }
    }

    public void StopHighlighting() {
        leftImage.SetActive(false);
        rightImage.SetActive(false);
        botImage.SetActive(false);
        topImage.SetActive(false);
    }

    // Use this for initialization
    void Start() {

        actionCounter = 0;
        objetoEntradaNombre.SetActive(false);
        m_methodCall = UnFadeGame;
        animalEntranceButton.onClick.AddListener(() => GoToNextEvent());
        acceptAnimalButton.onClick.AddListener(() => GoToNextEvent());
        resourceManagementButton.onClick.AddListener(() => GoToNextEvent());

        toggleGas.onValueChanged.AddListener(delegate {
            GoToNextEvent();
            resourceManagementWindow.SetActive(false);
        });
        toggleFood.onValueChanged.AddListener(delegate {
            GoToNextEvent();
            resourceManagementWindow.SetActive(false);
        });
        togglePublicity.onValueChanged.AddListener(delegate {
            GoToNextEvent();
            resourceManagementWindow.SetActive(false);
        });



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
        StopHighlighting();
    }

    public void UnFadeGame() {
        FaderScript.instance.unFade = true;
    }

    public void EmptyMethod() {

    }

    void PressEnterOrSpace(bool canContinue, TestDelegate Method1, TestDelegate Method2) {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return) || Input.GetMouseButtonDown(0)) {

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
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)||Input.GetMouseButtonDown(0)) {

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
        GameLogic.instance.timeForNextAnimal = 10000;
        if (!enabled) {
            if (GameLogic.instance != null && CanvasScript.canvasScript != null) {
                enabled = true;
            }
        } else {
            switch (actionCounter) {
                case 0:
                    StopHighlighting();
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
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
                        HighlightElement(ElementToHighlight.NONE);
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
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = textoEntradaNombre.text + " suena genial… Quizás esta vez sea la definitiva…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 3:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "¿Podrías encender la luz?";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 4:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "¡Espera un momento!";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 5:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Prométeme que no te asustarás...";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 6:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
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
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Hola, soy el inquilino más anciano de este refugio. Llevo aquí desde… Podríamos decir que desde el principio.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }


                    break;
                case 8:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "El nombre que me puso mi primer dueño fue Turtlellini, pero por aquí se me conoce como el anciano.";
                        StartCoroutine(DisplayText());
                        booleanOnce = false;
                    } else {
                        PressEnterOrSpace(true);


                    }

                    break;
                case 9:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Llevo tantos años entre vosotros que he llegado a aprender vuestro lenguaje.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }

                    break;
                case 10:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Cada vez que alguien toma el mando de este lugar yo me encargo de ayudarle.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 11:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Solo espero que…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;
                case 12:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        CanvasScript.canvasScript.enteringAnimalList.Clear();
                        currentText = "No… No tenemos tiempo que perder, hay muchísimo trabajo por delante.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }

                    break;

                case 13:

                    if (!displayingText) {

                        CanvasScript.canvasScript.AddEnteringAnimal();

                        CanvasScript.canvasScript.enteringAnimalList[0].edad = Animal.EDAD.CACHORRO;
                        CanvasScript.canvasScript.enteringAnimalList[0].estado = Animal.ESTADO.SALUDABLE;
                        CanvasScript.canvasScript.enteringAnimalList[0].salud = 80;
                        CanvasScript.canvasScript.enteringAnimalList[0].hambre = 5;

                        HighlightElement(ElementToHighlight.ENTRANCE_BUTTON);
                        currentText = "Mira!Parece que un pequeñajo te ha seguido hasta aquí";
                        StartCoroutine(DisplayText());
                    } else {
                        if (textDisplayer.text.Length == currentText.Length) {
                            //Highlight y desbloqueo Lista Animales

                        }
                        PressEnterOrSpace(false);

                        //PressEnterOrSpace(true, UnFadeGame, StartHideBubbleCoroutine);
                    }

                    break;

                case 14:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ANIMAL_ENTRANCE);
                        currentText = "Vaya, parece que ha tenido una vida bastante dura…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                        //Highlight historia animal
                    }
                    break;
                case 15:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.HUNGER_INDICATOR_ENTERING);
                        currentText = "Y tiene hambre!";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador hambre
                        PressEnterOrSpace(true);

                    }
                    break;
                case 16:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ACCEPT_BUTTON);
                        currentText = "Creo que deberíamos darle un hogar...";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador aceptar en refugio, desbloquear botón aceptar en refugio
                    }
                    break;
                case 17:
                    if(GameLogic.instance.shelterAnimals[0].transform.position.x!= 427+960 || GameLogic.instance.shelterAnimals[0].transform.position.y != -247 + 540) {
                        GameLogic.instance.shelterAnimals[0].transform.position = new Vector2(427+960,-247+540);
                    }
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.MONEY_ICON);
                        currentText = "Veo que vienes con unos ahorros";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                        //Resaltar indicador dineros
                    }
                    break;
                case 18:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Deberíamos empezar por comprar comida para el pequeñajo";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 19:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.MANAGEMENT_BUTTON);
                        currentText = "Usa tu teléfono para hacer un encargo de comida";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón gestion y desbloquear botón gestión
                        PressEnterOrSpace(false);

                    }
                    break;
                case 20:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.SMALL_FOOD_TOGGLE);

                        currentText = "Empezaremos por comprar 10 raciones, así tendremos suficiente para unos cuantos días";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar toggle poca comida y desbloquear toggle poca comida
                        PressEnterOrSpace(false);

                    }

                    break;
                case 21:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Por desgracia, tardan una semana en traer los pedidos";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón semana siguiente y desbloquear botón semana siguiente
                        PressEnterOrSpace(true);

                    }
                    break;
                case 22:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.WEEK_TIMER);
                        currentText = "Vamos a esperar";
                        StartCoroutine(DisplayText());
                    } else {
                        //Esconder y des esconder la burbuja
                        //Salto de semana automatico con fadeout fadein
                        PressEnterOrSpace(false);

                    }
                    break;
                case 23:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Vaya… Realmente lo está pasando bastante mal. Menos mal que el pedido ya ha llegado";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 24:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ACCEPTED_ANIMAL);
                        currentText = "Vamos, acércate a él, no tengas miedo…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar Animal y desbloquear click animal
                    }
                    break;
                case 25:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.FEED_BUTTON);

                        currentText = "Dale una ración. Se sentirá mucho mejor.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);

                        //Resaltar botón dar de comer y desbloquear botón dar de comer
                    }
                    break;
                case 26:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.HAPPINESS_INDICATOR);

                        currentText = "Eso es! Se le ve mucho más feliz.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);

                        //Resaltar indicador felicidad
                    }
                    break;
                case 27:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Seguramente no lo hubiera conseguido sin ti";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 28:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Vaya… Es extraño…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 29:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Tengo la sensación de que tenía que explicarte algo más";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 30:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Eso es! ¡Casi se me olvida!";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 31:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.MANAGEMENT_BUTTON);
                        currentText = "Volvamos al gestor de gastos";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);

                        //Resaltar boton gestion gastos
                    }
                    break;
                case 32:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Es el momento de ir a buscar nuevos inquilinos para el refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 33:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.SMALL_TOGGLE_GAS);
                        currentText = "Podríamos salir con la vieja furgoneta a buscar por las calles";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar toggle poca gasolina
                    }
                    break;
                case 34:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Tenemos que intentar ahorrar un poco…";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 35:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Creo que ya hemos hecho todo lo posible por esta semana";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 36:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.WEEK_TIMER);
                        currentText = "Vayamos a la siguiente";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton fin de semana
                        PressEnterOrSpace(false);

                    }
                    break;
                case 37:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ACCEPT_EVENT_BUTTON);
                        currentText = "Vaya! Parece que alguien ha conseguido algo de dinero. Muy bien hecho! Esto nos ayudará a salir a flote…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar boton aceptar evento
                        PressEnterOrSpace(false);

                    }
                    break;
                case 38:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ENTRANCE_BUTTON);
                        currentText = "Recuerda que los animales que rescatamos de las calles todavía están en la furgoneta! Vamos a echarles un vistazo";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón entrada animales
                        PressEnterOrSpace(false);

                    }
                    break;
                case 39:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ALL_ENTRANCES);
                        currentText = "Vaya! Mira cuantos nuevos amigos! Vamos a prepararles unas camas y dejarles entrar";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                //case 40:
                //    if (!displayingText) {

                //        currentText = "Tenemos sitio de sobra, deberíamos prepararles unas camas y dejarles entrar";
                //        StartCoroutine(DisplayText());
                //    } else {
                //        //Resaltar botones para acoger de todos
                //    }
                //    break;
                case 40:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.SICK_ANIMAL);
                        currentText = "Oh! Resulta que uno de ellos está algo enfermo, veamos qué podemos hacer…";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar animal enfermo
                        PressEnterOrSpace(false);

                    }
                    break;
                case 41:

                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.HEAL_BUTTON);

                        currentText = "Vamos a darle algo de medicina...";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);

                        //Resaltar medecina
                    }
                    break;


                case 42:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Bien hecho! Está como una rosa";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 43:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.ADOPTER);
                        currentText = "Oh vaya! Parece que tenemos visita!";
                        StartCoroutine(DisplayText());
                    } else {
                        //Generar adoptante
                        //Resaltar adoptante
                        PressEnterOrSpace(false);

                    }
                    break;
                case 44:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.GIVE_AWAY);

                        currentText = "Le echaremos de menos, pero al fin y al cabo es nuestra función, debemos cuidar de ellos hasta que encuentren un hogar mejor.Despídete de él, seguro que será muy feliz";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar botón entregar
                        PressEnterOrSpace(false);

                    }
                    break;
                case 45:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "No te pongas triste, él estaba muy agradecido por lo que has hecho por él, has hecho feliz a un animal y has hecho feliz a un humano.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 46:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.REPUTATION);
                        currentText = "Vaya! Además, parece que el adoptante ha recomendado el refugio a algunos conocidos, nuestra reputación ha aumentado.";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador reputación
                        PressEnterOrSpace(true);

                    }
                    break;
                case 47:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Esto nos ayudará, haciendo que venga más gente.";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 48:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "¡Ahora que lo recuerdo! Podemos hacer algo más para hacer que venga gente";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 49:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.MANAGEMENT_BUTTON);

                        currentText = "Vayamos de nuevo a la gestión de gastos";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar boton gastos
                    }
                    break;
                case 50:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.SMALL_TOGGLE_PUBLICITY);
                        currentText = "Vamos a hacer algo de publicidad";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar gasto mínimo de publicidad
                    }
                    break;
                case 51:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.SMALL_TOGGLE_PUBLICITY);

                        currentText = "Nos aseguraremos de que vengan más adoptantes durante la semana que viene";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                    }
                    break;
                case 52:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.TOTAL_EXPENSE);

                        currentText = "Ten en cuenta que cuantos más inquilinos tengas, más gastos básicos tendrás durante cada semana, esto incluye cosas como la limpieza, el agua, comprar juguetes, etc";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar gasto fijo
                    }
                    break;
                case 53:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.MONEY_ICON);

                        currentText = "Por lo que deberás tener cuidado en la forma que gastas el dinero";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(false);
                        //Resaltar indicador dineros
                    }
                    break;
                case 54:
                    if (!displayingText) {
                        //HighlightElement(ElementToHighlight.);
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Recuerda que deberás echar gasolina a la furgoneta para poder ir a buscar nuevos animales para el refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar toggle gasolina
                        PressEnterOrSpace(false);
                    }
                    break;
                case 55:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Aunque decidas no salir a buscar animales, de vez en cuando aparecerá alguno en la puerta del refugio";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);

                    }
                    break;
                case 56:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Pero es posible que haya un momento que necesitemos tener una entrada frecuente de inquilinos para poder satisfacer a los adoptantes";
                        StartCoroutine(DisplayText());
                    } else {
                        //Resaltar indicador dineros
                        PressEnterOrSpace(true);
                    }
                    break;
                case 57:
                    if (!displayingText) {
                        HighlightElement(ElementToHighlight.NONE);
                        currentText = "Ahora que ya sabes como funciona el refugio, te dejo al mando y me voy a dormir, Buena suerte!";
                        StartCoroutine(DisplayText());
                    } else {
                        PressEnterOrSpace(true);
                        //Resaltar indicador dineros
                    }
                    break;





            }
        }
    }
}
