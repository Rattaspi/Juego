using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnonymousDonerEvent : Event {
    float randomAmountOfMoney;
    public AnonymousDonerEvent() {
        float amountOfMoney = Random.Range(50, 200);
        description = "A mysterious figure dropped a bag on the entrance of the Shelter with " + amountOfMoney + " dollars inside";
        title = "Anonymous Donation";
        canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
        acceptMessage = "Pick up the money";
        declineMessage = "Give money to the authorities";

    }

    public override void OnAccept() {
        base.OnAccept();
        if (GameLogic.instance != null) {
            GameLogic.instance.AddMoney(randomAmountOfMoney);
        }
    }

    public override void OnDeny() {
        base.OnDeny();
    }

}
