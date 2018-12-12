using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventFeria : Event
{
    float randomAmountOfMoney;
    public EventFeria()
    {
        float amountOfMoney = 200;
        description = "Te han ofrecido un tenderete en una feria local de animales a cambio de" + amountOfMoney + "€. Esto podría traer muchos adoptantes";
        title = "Una feria bestial";
        canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
    }

    public override void OnAccept()
    {
        base.OnAccept();
        if (GameLogic.instance != null)
        {
            GameLogic.instance.money -= randomAmountOfMoney;
            GameLogic.instance.reputation += 5;
        }
    }

    public override void OnDeny()
    {
        base.OnDeny();
    }

}