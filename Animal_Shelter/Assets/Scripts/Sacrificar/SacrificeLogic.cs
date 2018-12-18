using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SacrificeLogic : MonoBehaviour {
    [SerializeField] Transform animalPosition;
    //[SerializeField] FaderScript fader;
    [SerializeField] GameObject mouseSyringe;
    [SerializeField] GraphicRaycaster graphicRaycaster;
    GameObject animalDisplayed;
    CanvasGroup group;

    private void Awake() {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
        group = GetComponentInChildren<CanvasGroup>();
    }

    private void OnEnable() {
        Animal animalSacr = GameLogic.instance.animalToSacrifice;
        GameObject animalDisplayed = Instantiate(GameLogic.instance.animalGraphics[(int)animalSacr.especie], animalPosition);
        TintAnimalPart[] t = animalDisplayed.GetComponentsInChildren<TintAnimalPart>();
        foreach(TintAnimalPart i in t){
            i.ForcePaint(animalSacr.color);
        }

        StartCoroutine(FaderScript.instance.UnFade());
    }

    private void OnDisable() {
        group.alpha = 1.0f;
        mouseSyringe.SetActive(false);
    }

    public void SacrificeClick() {
        print(mouseSyringe.activeInHierarchy);
        if (mouseSyringe.activeInHierarchy) {
            StartCoroutine(Sacrifice());
        }
    }

    IEnumerator Sacrifice() {
        graphicRaycaster.enabled = false;
        float animalFadeSpeed = 1.0f;
        while(group.alpha > 0.0f) {
            group.alpha -= animalFadeSpeed * Time.deltaTime;
            yield return null;
        }
        group.alpha = 0.0f;
        StartCoroutine(FaderScript.instance.Fade());
        while (FaderScript.instance.doing) {
            yield return null;
        }
        Destroy(animalDisplayed);
        GameLogic.instance.RemoveAnimal(GameLogic.instance.animalToSacrifice);
        GameLogic.instance.animalToSacrifice = null;
        graphicRaycaster.enabled = true;
        yield return new WaitForSeconds(1.0f);
        FaderScript.instance.unFade = true;
        this.gameObject.SetActive(false);

    }
}
