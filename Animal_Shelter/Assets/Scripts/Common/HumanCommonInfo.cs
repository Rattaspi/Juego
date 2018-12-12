using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HumanCommonInfo {
    static string[] names = { "Lucas", "Hugo", "Martín", "Daniel", "Pablo", "Alejandro", "Alex", "Mateo", "Adrián", "Álvaro", "Manuel", "David", "Mario", "Diego", "Javier", "Marcos", "Carlos", "Antonio", "Miguel", "Gonzalo", "Jorge", "Lucía", "Sofía", "María", "Martina", "Paula", "Julia", "Daniela", "Valeria", "Alba", "Emma", "Carla", "Sara", "Noa", "Carmen", "Claudia", "Valentina", "Alma", "Ana", "Chloe", "Marta" };

    public static string GetName() {
        return names[Random.Range(0, names.Length)];
    }
}
