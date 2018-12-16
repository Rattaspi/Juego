using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class CanvasScriptTutorial : MonoBehaviour /*CanvasScript*/ {

    //void Start() {
    //    animalElementPrefab = Resources.Load<GameObject>("Prefabs/AnimalElementListPrefab");
    //    canvasScript = this;
    //    UIManagementGameObject.SetActive(true);
    //    eventHandlerObject.SetActive(false);
    //    //incomingAnimalListObject.SetActive(false);
    //    expensesWindow.SetActive(false);
    //    animalsWindow.SetActive(false);
    //    tempAnimalParent = new GameObject();
    //    tempAnimalParent.transform.position = new Vector3(-1300, 0, 0);
    //    tempAnimalParent.transform.localPosition = new Vector3(-1300, 0, 0);

    //    tempAnimalParent.name = "tempAnimalParent";
    //    tempAnimalParent.transform.parent = gameObject.transform;
    //    canvasState = CanvasState.IDLE;
    //    enteringAnimalButtonObject.SetActive(false);
    //    enteringAnimalList = new List<Animal>();

    //    imageChildren = noSpaceMessageGroup.GetComponentsInChildren<Image>();
    //    textChildren = noSpaceMessageGroup.GetComponentsInChildren<TextMeshProUGUI>();
    //    noSpaceAlphaValue = 0.01f;
    //}

    //// Update is called once per frame
    //void Update() {

    //    amountOfAnimalsFeedBackText.text = GameLogic.instance.shelterAnimals.Count.ToString() + "/" + GameLogic.instance.currentAnimalCapacity;

    //    if (noSpaceAlphaValue > 0) {
    //        noSpaceAlphaValue -= Time.deltaTime;

    //        if (noSpaceAlphaValue < 0) {
    //            noSpaceAlphaValue = 0;
    //        }

    //        for (int i = 0; i < imageChildren.Length; i++) {
    //            imageChildren[i].color = new Color(imageChildren[i].color.r, imageChildren[i].color.g, imageChildren[i].color.b, noSpaceAlphaValue);
    //        }

    //        for (int i = 0; i < textChildren.Length; i++) {
    //            textChildren[i].color = new Color(textChildren[i].color.r, textChildren[i].color.g, textChildren[i].color.b, noSpaceAlphaValue);

    //        }

    //    }

    //    if (Input.GetKeyDown(KeyCode.Space)) {
    //        debugBool = true;
    //    }

    //    if (debugBool) {
    //        debugBool = false;
    //        canvasScript.AddEnteringAnimal();
    //    }
    //    enteringAnimalsNumber.text = canvasScript.enteringAnimalList.Count.ToString();
    //    switch (canvasState) {
    //        case CanvasState.DISPLAYANIMALLIST:
    //            if (selectedAnimalDisplayer != null) {
    //                if (selectedAnimalDisplayer.selectedAnimalInList != animalOnListClickedOnto) {
    //                    selectedAnimalDisplayer.SetInfo(animalOnListClickedOnto);
    //                }
    //            }
    //            break;
    //        case CanvasState.IDLE:
    //            totalExpensesText.text = "Total : " + CommonMethods.GetNumberWithDots((int)GameLogic.instance.GetToggleOptionsMoney()) + "€";
    //            foodText.text = "Compra de comida (" + GameLogic.instance.currentFoodExpense.ToString() + "€)";
    //            publicityText.text = "Gastos en publicidad (" + GameLogic.instance.currentPublicityExpense.ToString() + "€)";
    //            instalationsText.text = "Gastos local (" + GameLogic.instance.currentInstalationsExpense.ToString() + "€)";
    //            cleanUpText.text = "Limpieza (" + GameLogic.instance.currentCleanUpExpense.ToString() + "€)";
    //            truckText.text = "Gastos en furgoneta (busca animales) (" + GameLogic.instance.currentGasExpense.ToString() + "€)";

    //            moneyText.text = CommonMethods.GetNumberWithDots((int)GameLogic.instance.money) + "€";
    //            break;
    //        default:
    //            break;

    //    }
    //}

    //private void OnDestroy() {
    //    canvasScript = null;
    //}
    
}
