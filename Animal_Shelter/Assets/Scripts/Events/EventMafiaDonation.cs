using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventMafiaDonation : Event
{
    float randomAmountOfMoney;
    public EventMafiaDonation()
    {
        float amountOfMoney = Random.Range(50, 200);
        description = "Un tipo sospechoso aparece por la puerta./n-¡Psst! ¿Te importaría guardarme este dinero? Vendré a recogerlo más adelante.";
        title = "El hombre del sombrero negro";
        //canBeDenied = false;
        randomAmountOfMoney = amountOfMoney;
        acceptMessage = "Aceptar " + amountOfMoney + "€";
        declineMessage = "Rechazar " + amountOfMoney + "€";
    }

    public override void OnAccept()
    {
        base.OnAccept();
        if (GameLogic.instance != null)
        {
            GameLogic.instance.AddMoney(randomAmountOfMoney);
            GameLogic.instance.reputation -= 1;
        }
    }

    public override void OnDeny()
    {
        
        base.OnDeny();
        if (GameLogic.instance != null)
        {
            GameLogic.instance.reputation += 3;
        }
    }

}