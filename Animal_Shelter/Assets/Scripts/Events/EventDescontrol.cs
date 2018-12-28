using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDescontrol : Event
{
    float randomAmountOfMoney;
    public EventDescontrol()
    {
        float amountOfMoney = Random.Range(50, 250);
        description = "¡Un animal se ha descontrolado! Parece que vas a tener que reparar algunos muebles. Te va a costar " + amountOfMoney + "€.";
        title = "¡Se ha vuelto loco!";
        canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
        acceptMessage = "Reparar";
        //declineMessage = "Rechazar " + amountOfMoney + "€";
    }

    public override void OnAccept()
    {
        base.OnAccept();
        if (GameLogic.instance != null)
        {
            GameLogic.instance.AddMoney(randomAmountOfMoney);
        }
    }

    public override void OnDeny()
    {
        base.OnDeny();
    }

}