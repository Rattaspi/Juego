using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownHallMoneyEvent : Event {
    float randomAmountOfMoney;
    public TownHallMoneyEvent() {
        float amountOfMoney = Random.Range(50, 200);
        description = "The local elections are near and the town hall wants to make a good impresion, so they decide to donate " + amountOfMoney + " euros to your shelter";
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
