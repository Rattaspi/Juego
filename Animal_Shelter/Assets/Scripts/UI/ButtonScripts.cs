﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour {

    public enum ButtonFunction { ENDWEEK, PAUSE, SACRIFICE, FEED, HEAL, DISPLAYEXPENSES, DISPLAYANIMALS,ACCEPTANIMAL,REJECTANIMAL,SET_ENTRY_INFO };
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

            case ButtonFunction.SACRIFICE:
                myselfButton.onClick.AddListener(() => SacrificeAnimal());
                break;

            case ButtonFunction.FEED:
                myselfButton.onClick.AddListener(() => FeedAnimal());
                break;

            case ButtonFunction.HEAL:
                myselfButton.onClick.AddListener(() => HealAnimal());
                break;

            case ButtonFunction.DISPLAYEXPENSES:
                myselfButton.onClick.AddListener(() => DisplayExpenses());
                break;

            case ButtonFunction.DISPLAYANIMALS:
                myselfButton.onClick.AddListener(() => DisplayAnimals());
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
            default:
                break;
        }

    }

    void SetEntryInfo() {

        CanvasScript.canvasScript.enteringAnimalDisplayer.SetInfo(CanvasScript.canvasScript.enteringAnimalDisplayer.selectedAnimalInList);
    }

    void ResolveAnimalRequest(bool accepted) {
        if (accepted) {
            CanvasScript.canvasScript.ResolveAnimalRequest(accepted);
        } else {
            CanvasScript.canvasScript.ResolveAnimalRequest(accepted);

        }
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
            if (GameLogic.instance.CanEndWeek()) {
                GameLogic.instance.EndWeek();
            }
        }
    }

    void SacrificeAnimal() {
        throw new NotImplementedException();
    }

    void FeedAnimal() {
        throw new NotImplementedException();
    }

    void HealAnimal() {
        throw new NotImplementedException();
    }

    void DisplayExpenses() {
        if (CanvasScript.canvasScript != null) {
            CanvasScript.canvasScript.DisplayExpenses();
        }
    }

    void DisplayAnimals() {
        if (CanvasScript.canvasScript != null) {
            CanvasScript.canvasScript.DisplayShelterAnimals();
        }
    }
}
