using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAnimaltMoribundo : Event {
    GameObject randomNewAnimal;
    public float reputationAtStake;
    public EventAnimaltMoribundo() {
        randomNewAnimal = Animal.MakeARandomAnimal();
        randomNewAnimal.GetComponent<Animal>().salud = 2;
        randomNewAnimal.GetComponent<Animal>().estado= Animal.ESTADO.TERMINAL;

        //randomNewAnimal = 
        description = "Aparece en tu puerta un " + randomNewAnimal.GetComponent<Animal>().especie.ToString().ToLower() + " moribundo.. necesitára de muchos cuidados para sobrevivir..";
        title = "Se está muriendo..";
        canBeDenied = true;
        acceptMessage = "Nos haremos cargo!";
        declineMessage = "No podemos hacernos cargo..."; 
    }

    public override void OnAccept() {
        CanvasScript.canvasScript.AcceptAnimalInShelter(randomNewAnimal.GetComponent<Animal>());
        GameLogic.instance.reputation += reputationAtStake;
        base.OnAccept();
    }

    public override void OnDeny() {
        GameLogic.instance.RemoveAnimal(randomNewAnimal.GetComponent<Animal>());
        GameLogic.instance.reputation -= reputationAtStake;
        base.OnDeny();
    }


}
