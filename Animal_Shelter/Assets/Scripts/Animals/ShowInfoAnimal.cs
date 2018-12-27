using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShowInfoAnimal : MonoBehaviour {
    Animal animalInfo;
    AnimalGraphics graphics;
    [SerializeField] Text name;
    [SerializeField] Image cara;
    [SerializeField] Image salud;
    [SerializeField] Image comida;
    [SerializeField] Image edad;
    Sprite[] faces;

    GraphicRaycaster graphicRaycaster;
    EventSystem eventSystem;
    PointerEventData pointerEvent;

    void Awake () {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        if (graphicRaycaster == null) Debug.LogError("Graphic raycaster not found from ShowInfoAnimal.cs");
        eventSystem = FindObjectOfType<EventSystem>();
        if (eventSystem == null) Debug.LogError("EventSystem not found from ShowInfoAnimal.cs");

        animalInfo = GetComponentInParent<Animal>();
        if (animalInfo == null) Debug.LogError("Animal script not found");

        graphics = GetComponentInParent<AnimalGraphics>();

        //Get the faces sprites
        faces = new Sprite[3];
        for (int i = 0; i < 3; i++) {
            faces[i] = Resources.Load<Sprite>("Sprites/AnimalPreviewInfo/cara_" + i);
        }
    }

    void OnEnable () {
        name.text = animalInfo.nombre;
        cara.sprite = faces[(int)animalInfo.confort];
        salud.fillAmount =  1 - Mathf.InverseLerp(0, (int)Animal.ESTADO.LENGTH - 1, (int)animalInfo.estado);
        comida.fillAmount = Mathf.InverseLerp(0, 10, animalInfo.hambre);
        edad.fillAmount = 0.1f + Mathf.InverseLerp(0, (int)Animal.EDAD.LENGTH - 1, (int)animalInfo.edad);

        animalInfo.AnimalClicked();
    }

    private void OnDisable() {
        animalInfo.ResetAnimalPosition();
    }

    public void UpdateInfo() {
        //print("update");
        OnEnable();
    }

    public void ActivateChangeName() {
        graphics.changeName.SetActive(true);
    }

    public void Sacrifice() {
        GameLogic.instance.animalToSacrifice = animalInfo;
        animalInfo.ResetAnimalPosition();
        StartCoroutine(FaderScript.instance.Fade());
        StartCoroutine(InitSacrifice());        
    }

    IEnumerator InitSacrifice() {
        while (FaderScript.instance.doing) {
            yield return null;
        }
        GameLogic.instance.sacrificeScreen.SetActive(true);
        StartCoroutine(FaderScript.instance.UnFade());
    }
}
