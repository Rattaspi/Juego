using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Debug_Adoptante : MonoBehaviour {
    [Header("Adoptante")]
    [SerializeField] bool generateAdopter = false;
	
	void Update () {
        if (generateAdopter) {
            GenerateAdopter();
            generateAdopter = false;
        }
	}

    void GenerateAdopter() {
        GameObject g = new GameObject();
        g.name = "Adoptante";
        g.transform.parent = this.transform;
        g.AddComponent(typeof(Adoptante));
    }
}
