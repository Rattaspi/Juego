using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HumanCommonInfo {
    #region NAMES
    static string[] names = { "Lucas", "Hugo", "Martín", "Daniel", "Pablo", "Alejandro", "Alex", "Mateo", "Adrián", "Álvaro", "Manuel", "David", "Mario", "Diego", "Javier", "Marcos", "Carlos", "Antonio", "Miguel", "Gonzalo", "Jorge", "Lucía", "Sofía", "María", "Martina", "Paula", "Julia", "Daniela", "Valeria", "Alba", "Emma", "Carla", "Sara", "Noa", "Carmen", "Claudia", "Valentina", "Alma", "Ana", "Chloe", "Marta" };

    public static string GetName() {
        return names[Random.Range(0, names.Length)];
    }
    #endregion

    #region ADOPTION_REPLIES
    static string[] acceptMessage = {
        "¡Muchas gracias! Es perfecto.",
        "¡Esto es exactamente lo que buscaba!",
        "¡Es como lo habia imaginado!",
        "¡Me lo quedo!"
    };

    static string[] declineMessage = {
        "¡Que animal más feo! No lo quiero.",
        "Paso. Adiós.",
        "No es lo que buscaba. Lo siento.",
        "Iré a otro sitio. Adiós."
    };

    public static string GetAcceptMessage() {
        return acceptMessage[Random.Range(0, acceptMessage.Length)];
    }

    public static string GetRejectMessage() {
        return declineMessage[Random.Range(0, declineMessage.Length)];
    }
    #endregion
}
