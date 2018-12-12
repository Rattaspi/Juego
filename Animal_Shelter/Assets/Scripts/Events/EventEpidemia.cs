using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventEpidemia : Event
{
    float randomAmountOfMoney;
    public EventEpidemia()
    {
        description = "Parece que una plaga ha invadido la ciudad y se ha introducido en el refugio... ¡Tus animales se han puesto enfermos!";
        title = "Epidemia";
        canBeDenied = false;
        acceptMessage = "Vaya...";
        //declineMessage = "Rechazar " + amountOfMoney + "€";
    }

    public override void OnAccept()
    {
        base.OnAccept();
        if (GameLogic.instance != null)
        {
            //GameLogic.instance.money += randomAmountOfMoney;
            int totalAnimals = GameLogic.instance.shelterAnimals.Count;
            int sickAnimals = 0;
            foreach (Animal a in GameLogic.instance.shelterAnimals)
            {
                if (totalAnimals / sickAnimals < 3)
                {
                    int randomNum = Random.Range(0, 10);
                    if (randomNum < 6 && a.salud >= 45)
                    {
                        a.salud = 30;
                        sickAnimals++;
                    }
                }

            }
        }
    }

    public override void OnDeny()
    {
        base.OnDeny();
    }

}
