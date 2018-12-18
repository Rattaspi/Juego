using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeLogic : MonoBehaviour {
    [SerializeField] Transform animalPosition;
    [SerializeField] FaderScript fader;
    [SerializeField] GameObject mouseSyringe;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    GameObject animalDisplayed;

    private void Awake() {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    private void OnEnable() {
        Animal animalSacr = GameLogic.instance.animalToSacrifice;
        GameObject animalDisplayed = Instantiate(GameLogic.instance.animalGraphics[(int)animalSacr.especie], animalPosition);
        TintAnimalPart[] t = animalDisplayed.GetComponentsInChildren<TintAnimalPart>();
        foreach(TintAnimalPart i in t){
            i.ForcePaint(animalSacr.color);
        }

        StartCoroutine(fader.UnFade());
    }

    public void SacrificeClick() {
        print(mouseSyringe.activeInHierarchy);
        if (mouseSyringe.activeInHierarchy) {
            StartCoroutine(Sacrifice());
        }
    }

    IEnumerator Sacrifice() {
        graphicRaycaster.enabled = false;
        CanvasGroup group = animalPosition.gameObject.AddComponent<CanvasGroup>();
        float animalFadeSpeed = 1.0f;
        while(group.alpha > 0.0f) {
            group.alpha -= animalFadeSpeed * Time.deltaTime;
            yield return null;
        }
        group.alpha = 0.0f;
        StartCoroutine(fader.Fade());
        while (fader.doing) {
            yield return null;
        }
        Destroy(animalDisplayed);
        GameLogic.instance.RemoveAnimal(GameLogic.instance.animalToSacrifice);
        GameLogic.instance.animalToSacrifice = null;
        graphicRaycaster.enabled = true;

        this.gameObject.SetActive(false);
    }
}
