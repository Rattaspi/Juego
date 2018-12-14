using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEscapePerros : Event {
    List<Animal> runAwayList;
    public EventEscapePerros() {
        runAwayList = new List<Animal>();

        int runAwayChances = 3;

        for (int i = 0; i < GameLogic.instance.shelterAnimals.Count && runAwayList.Count < Mathf.FloorToInt(GameLogic.instance.shelterAnimals.Count / 3); i++) {
            int randomInt = Random.Range(0, 9);
            if (randomInt < runAwayChances) {
                runAwayList.Add(GameLogic.instance.shelterAnimals[i]);
            }
        }

        description = runAwayList.Count.ToString() + " animales se han escapado durante la noche!";
        title = "¡La gran evasión!";
        canBeDenied = false;
        //randomAmountOfMoney = amountOfMoney;
        acceptMessage = "Vale..";
        //declineMessage = "Rechazar " + amountOfMoney + "€";
    }

    public override void OnAccept() {
        foreach (Animal a in runAwayList) {
            if (GameLogic.instance.shelterAnimals.Contains(a)) {
                GameLogic.instance.RemoveAnimal(a);
            }
        }
        runAwayList.Clear();
        base.OnAccept();

    }

    public override void OnDeny() {
        base.OnDeny();
    }
}
