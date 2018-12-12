using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventReportaje : Event
{
    float randomAmountOfMoney;
    public EventReportaje()
    {
        float amountOfMoney = 200;
        description = "Ha venido una reportera por sorpresa y te ofrece realizar una entrevista para una cadena local. ¿Estás preparado?";
        title = "Reportaje sorpresa";
        //canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
    }

    public override void OnAccept()
    {
        base.OnAccept();
        if (GameLogic.instance != null)
        {

            //Recorrer animales y ver su felicidad, si estan felices te darán reputación, si no lo estan te la quitaran
        }
    }

    public override void OnDeny()
    {
        base.OnDeny();
        if (GameLogic.instance != null)
        {
            GameLogic.instance.reputation -= 3;
        }
    }

}