using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

    public enum ButtonFunction { ENDWEEK, PAUSE, SACRIFICE, FEED, HEAL, DISPLAYEXPENSES, DISPLAYANIMALS,ACCEPTANIMAL,REJECTANIMAL,SET_ENTRY_INFO, INCREASE_ANIMAL_INDEX,DECREASE_ANIMAL_INDEX,STOP_DISPLAY_ANIMALS, STOP_DISPLAY_EXPENSES,TUTORIAL_OVERRIDER_NEXTEVENT };
    public ButtonFunction buttonFunction;
    private Button myselfButton;

    void Start() {
        myselfButton = GetComponent<Button>();
        switch (buttonFunction) {
            case ButtonFunction.ENDWEEK:
                myselfButton.onClick.AddListener(() => EndWeek());
                break;
            case ButtonFunction.PAUSE:
                myselfButton.onClick.AddListener(() => Pause());
                break;

            case ButtonFunction.FEED:
                myselfButton.onClick.AddListener(() => FeedAnimal());
                break;

            case ButtonFunction.HEAL:
                myselfButton.onClick.AddListener(() => HealAnimal());
                break;

            case ButtonFunction.DISPLAYEXPENSES:
                myselfButton.onClick.AddListener(() => DisplayExpenses(true));
                break;

            case ButtonFunction.DISPLAYANIMALS:
                myselfButton.onClick.AddListener(() => DisplayAnimals(true));
                break;

            case ButtonFunction.ACCEPTANIMAL:
                myselfButton.onClick.AddListener(() => ResolveAnimalRequest(true));
                break;
            case ButtonFunction.REJECTANIMAL:
                myselfButton.onClick.AddListener(() => ResolveAnimalRequest(false));
                break;
            case ButtonFunction.SET_ENTRY_INFO:
                myselfButton.onClick.AddListener(() => SetEntryInfo());
                break;
            case ButtonFunction.INCREASE_ANIMAL_INDEX:
                myselfButton.onClick.AddListener(() => IncreaseAnimalIndex(1));

                break;
            case ButtonFunction.DECREASE_ANIMAL_INDEX:
                myselfButton.onClick.AddListener(() => IncreaseAnimalIndex(-1));
                break;
            case ButtonFunction.STOP_DISPLAY_ANIMALS:
                myselfButton.onClick.AddListener(() => DisplayAnimals(false));
                break;
            case ButtonFunction.STOP_DISPLAY_EXPENSES:
                myselfButton.onClick.AddListener(() => DisplayExpenses(false));
                break;
            case ButtonFunction.TUTORIAL_OVERRIDER_NEXTEVENT:
                myselfButton.onClick.AddListener(() => SetNameForShelter());
                break;
            default:
                break;
        }

    }

    void IncreaseAnimalIndex(int increase) {
        CanvasScript.canvasScript.IncreaseDisplayIndex(increase);
    }

    IEnumerator SetEntryInfoCoroutine() {
        yield return null;

        //CanvasScript.canvasScript.enteringAnimalDisplayer.SetInfo(CanvasScript.canvasScript.enteringAnimalDisplayer.selectedAnimalInList);
        //CanvasScript.canvasScript.IncreaseDisplayIndex(0);
        CanvasScript.canvasScript.SetDisplayIndex(0);
    }

    void SetEntryInfo() {
        StartCoroutine(SetEntryInfoCoroutine());
    }

    void ResolveAnimalRequest(bool accepted) {
            CanvasScript.canvasScript.ResolveAnimalRequest(accepted);
    }

    void Pause() {
        GameTime.Pause();
    }

    void DisplayEnteringAnimal() {
        if (CanvasScript.canvasScript != null) {
            CanvasScript.canvasScript.DisplayEnteringAnimal();
        }
    }

    void EndWeek() {
        if (GameLogic.instance != null) {
                GameLogic.instance.EndWeek();
        }
    }
    void FeedAnimal() {

        if (TutorialOverrider.instance != null){
            TutorialOverrider.instance.GoToNextEvent();
        }

        Animal animal = GetComponentInParent<Animal>();
        if (animal != null) {
            if (GameLogic.instance.amountOfFood > animal.gastoComida) {
                if (animal.hambre < 30) {
                    GameLogic.instance.amountOfFood -= animal.gastoComida;
                    animal.FeedAnimal();
                    animal.UpdateDisplayedAnimalInfo();
                } else {
                    CanvasScript.canvasScript.PopUpNoSpaceMessage(animal.nombre + " no tiene hambre");
                }
            } else {
                CanvasScript.canvasScript.PopUpNoSpaceMessage("No queda comida para alimentar a " + animal.nombre);
            }
        }


            //throw new NotImplementedException();
        }

    void HealAnimal() {

        if (TutorialOverrider.instance != null) {
            TutorialOverrider.instance.GoToNextEvent();
        }

        Animal animal = GetComponentInParent<Animal>();
        if (animal != null) {
            if (animal.salud < 100) {
                if (GameLogic.instance.money > GameLogic.instance.medicinePrice) {
                    GameLogic.instance.money -= GameLogic.instance.medicinePrice;
                    animal.TryHealing();
                    animal.UpdateDisplayedAnimalInfo();
                } else {
                    CanvasScript.canvasScript.PopUpNoSpaceMessage("No puedes permitirte curar a este animal");
                }
            } else {
                CanvasScript.canvasScript.PopUpNoSpaceMessage("Este animal está perfectamente");
            }
        } else {
            Debug.Log("Null");
        }

        //throw new NotImplementedException();
    }

    void DisplayExpenses(bool should) {
        if (CanvasScript.canvasScript != null) {
            CanvasScript.canvasScript.DisplayExpenses(should);
        }
    }

    void DisplayAnimals(bool should) {
        if (CanvasScript.canvasScript != null) {
            CanvasScript.canvasScript.DisplayShelterAnimals(should);
        }
    }

    void SetNameForShelter() {
        if (TutorialOverrider.instance != null) {
            if (TutorialOverrider.instance.textoEntradaNombre.text.Length >= 4) {
                TutorialOverrider.instance.GoToNextEvent();
                TutorialOverrider.instance.objetoEntradaNombre.SetActive(false);
                gameObject.SetActive(false);
            }
        }
    }

}
