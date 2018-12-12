using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallMoneyEvent : Event {
    float randomAmountOfMoney;
    public TownHallMoneyEvent() {
        float amountOfMoney = 200;
        description = "Las elecciones locales están muy cerca y el ayuntamiento ha decidido ayudar a unos cuantos negocios del pueblo con una subvención de " + amountOfMoney + ". Tu refugio ha sido uno de los seleccionados.";
        title = "Donación del ayuntamiento";
        canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
    }

    public override void OnAccept() {
        base.OnAccept();
        if (GameLogic.instance != null) {
            GameLogic.instance.money += randomAmountOfMoney;
        }
    }

    public override void OnDeny() {
        base.OnDeny();
    }

}
